using System.Text.Json.Serialization;
using EStockFlow.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EStockFlow.Models
{
    public class UpdateProductRequest
    {
        [JsonIgnore]
        [FromRoute(Name = "itemId")]
        public Guid Id { get; set; }
        
        [FromForm]
        public string Name { get; set; } = string.Empty;
        
        [FromForm]
        public decimal Price { get; set; }
        
        [FromForm]
        public int Stock { get; set; }
        
        [FromForm]
        public ProductCategoryEnum Category { get; set; }
        
        public IFormFile? Image { get; set; }
    }
}