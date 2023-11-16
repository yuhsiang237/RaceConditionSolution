public class Program
{
    private static int counter = 0;
    private static int value = 0;
    private static readonly string counterLock = "counterLock";
    static void Main()
    {
        Thread thread1 = new Thread(Inc);
        Thread thread2 = new Thread(Dec);

        // `The Thread.Start` method is used to initiate the execution of a new thread, starting with the specified method.
        thread1.Start();
        thread2.Start();

        // `Thread.Join` method is used to wait for a thread to complete.
        thread1.Join();
        thread2.Join();
    }

    /// <summary>
    /// To display current params
    /// </summary>
    /// <param name="msg"></param>
    static void DisplayMsg(string msg)
    {
        Thread.Sleep(10);
        Console.WriteLine($"{msg.PadRight(25)}value:{value} counter:{counter}");
    }

    /// <summary>
    /// To inc the value
    /// </summary>
    static void Inc()
    {
        // Monitor is a class that provides a mechanism for mutual exclusion, allowing you to synchronize access to a block of code or a critical section.
        // Enter the critical section
        Monitor.Enter(counterLock);
        try
        {
            value = counter;
            DisplayMsg("inc:value = counter");
            value = value + 1;
            DisplayMsg("inc:value = value + 1 ");
            counter = value;
            DisplayMsg("inc:counter = value");
        }
        finally
        {
            // Exit the critical section
            Monitor.Exit(counterLock);
        }
    }

    /// <summary>
    /// To dec the value
    /// </summary>
    static void Dec()
    {
        // Monitor is a class that provides a mechanism for mutual exclusion, allowing you to synchronize access to a block of code or a critical section.
        // Enter the critical section
        Monitor.Enter(counterLock);
        try
        {
            value = counter;
            DisplayMsg("dec:value = counter");
            value = value - 1;
            DisplayMsg("dec:value = value - 1 ");
            counter = value;
            DisplayMsg("dec:counter = value");
        }
        finally
        {
            // Exit the critical section
            Monitor.Exit(counterLock);
        }
    }

    //Output:
    //inc:value = counter value:0 counter:0
    //inc:value = value + 1    value:1 counter:0
    //inc:counter = value value:1 counter:1
    //dec:value = counter value:1 counter:1
    //dec:value = value - 1    value:0 counter:1
    //dec:counter = value value:0 counter:0

}