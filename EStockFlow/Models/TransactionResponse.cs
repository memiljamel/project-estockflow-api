using System.Text.Json.Serialization;
using EStockFlow.Enums;

namespace EStockFlow.Models
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        
        public dynamic Item { get; set; }
        
        public int Quantity { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ProductCategoryEnum Category { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal Price { get; set; }
        
        public decimal Amount { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}