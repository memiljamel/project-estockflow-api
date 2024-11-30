using EStockFlow.Enums;

namespace EStockFlow.Models
{
    public class CreateProductRequest
    {
        public string? Name { get; set; }
        
        public decimal Price { get; set; }
        
        public int InitialStock { get; set; }
        
        public ProductCategory Category { get; set; }
        
        public IFormFile Image { get; set; }
    }
}