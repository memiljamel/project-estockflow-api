﻿using System.Text.Json.Serialization;
using EStockFlow.Enums;

namespace EStockFlow.Models
{
    public class UpdateProductRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        public string? Name { get; set; }
        
        public decimal Price { get; set; }
        
        public int Stock { get; set; }
        
        public ProductCategoryEnum Category { get; set; }
        
        public IFormFile? Image { get; set; }
    }
}