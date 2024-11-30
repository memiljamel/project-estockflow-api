using EStockFlow.Enums;

namespace EStockFlow.Models
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        
        public string? Name { get; set; }
        
        public decimal Price { get; set; }
        
        public int InitialStock { get; set; }
        
        public ProductCategory Category { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}