using Microsoft.AspNetCore.Mvc;

namespace PreventRaceConditionWithSQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ProductManage _productManage;

        public StockController(ProductManage productManage)
        {
            _productManage = productManage;
        }

        [HttpGet("SubStockQuanity")]
        public async Task<bool> SubStockQuanityAsync(int productId, int subQuanity)
        {
            return await _productManage.UpdateProductStockAsync(productId, subQuanity);
        }
    }
}