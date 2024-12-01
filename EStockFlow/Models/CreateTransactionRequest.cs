using EStockFlow.Enums;

namespace EStockFlow.Models
{
    public class CreateTransactionRequest
    {
        public Guid Item { get; set; }
        
        public int Quantity { get; set; }
        
        public ProductCategoryEnum Category { get; set; }
        
        public decimal Price { get; set; }
    }
}