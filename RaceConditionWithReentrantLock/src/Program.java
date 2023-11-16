import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

public class Program {
    private static int counter = 0;
    private static int value = 0;
    private static final Lock counterLock = new ReentrantLock();

    public static void main(String[] args) {
        Thread thread1 = new Thread(Program::inc);
        Thread thread2 = new Thread(Program::dec);

        thread1.start();
        thread2.start();

        try {
            thread1.join();
            thread2.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    /**
     * To display current params
     */
    static void displayMsg(String msg) {
        try {
            Thread.sleep(10);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        System.out.println(String.format("%-25s value:%d counter:%d", msg, value, counter));
    }

    /**
     * To inc the value
     */
    static void inc() {
        // ReentrantLock is used for mutual exclusion.
        counterLock.lock();
        try {
            value = counter;
            displayMsg("inc:value = counter");
            value = value + 1;
            displayMsg("inc:value = value + 1 ");
            counter = value;
            displayMsg("inc:counter = value");
        } finally {
            // Ensure the lock is released, even if an exception occurs
            counterLock.unlock();
        }
    }

    /**
     * To dec the value
     */
    static void dec() {
        // ReentrantLock is used for mutual exclusion.
        counterLock.lock();
        try {
            value = counter;
            displayMsg("dec:value = counter");
            value = value - 1;
            displayMsg("dec:value = value - 1 ");
            counter = value;
            displayMsg("dec:counter = value");
        } finally {
            // Ensure the lock is released, even if an exception occurs
            counterLock.unlock();
        }
    }
    
//  dec:value = counter       value:0 counter:0
//	dec:value = value - 1     value:-1 counter:0
//	dec:counter = value       value:-1 counter:-1
//	inc:value = counter       value:-1 counter:-1
//	inc:value = value + 1     value:0 counter:-1
//	inc:counter = value       value:0 counter:0
}