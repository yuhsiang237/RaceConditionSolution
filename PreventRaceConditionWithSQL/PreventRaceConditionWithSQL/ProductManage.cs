using System.Data.SqlClient;

using Dapper;

namespace PreventRaceConditionWithSQL
{
    public class ProductManage
    {
        private readonly string _DBConnectString = @"Server=.\SQLExpress;Database=Factory;Trusted_Connection=True;ConnectRetryCount=0";

        /// <summary>
        /// UpdateProductStock
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quanity"></param>
        public async Task<bool> UpdateProductStockAsync(int productId, int quanity)
        {
            #region SQL atomic avoids race conditions
            var sql = @"
                UPDATE [dbo].[ProductStock]
                SET
	                Quanity = Quanity - @Quanity
                WHERE 
	                ProductId = @ProductId
	                AND Quanity - @Quanity >= @MinQuanity 
                ";
            #endregion

            using (var conn = new SqlConnection(_DBConnectString))
            {
                var rsCount = await conn.ExecuteAsync(sql, new
                {
                    Quanity = quanity,
                    ProductId = productId,
                    MinQuanity = 0,
                });

                return rsCount == 1;
            }
        }
    }
}