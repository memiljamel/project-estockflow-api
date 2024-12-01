using EStockFlow.Enums;

namespace EStockFlow.Models
{
    public class CreateProductRequest
    {
        public string? Name { get; set; }
        
        public decimal Price { get; set; }
        
        public int Stock { get; set; }
        
        public ProductCategoryEnum Category { get; set; }
        
        public IFormFile Image { get; set; }
    }
}