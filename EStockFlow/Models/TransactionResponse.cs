using EStockFlow.Enums;

namespace EStockFlow.Models
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        
        public Guid Item { get; set; }
        
        public int Quantity { get; set; }
        
        public ProductCategory Category { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal Amount { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}