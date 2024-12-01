using System.Text.Json.Serialization;
using EStockFlow.Enums;

namespace EStockFlow.Models
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        
        public string? Name { get; set; }
        
        public decimal Price { get; set; }
        
        public int Stock { get; set; }
        
        public ProductCategoryEnum Category { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ImageUrl { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime CreatedAt { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime UpdatedAt { get; set; }
    }
}