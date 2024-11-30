using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EStockFlow.Enums;

namespace EStockFlow.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        [MaxLength(100)]
        public string? Name { get; set; }
        
        [Required]
        [Column(Order = 2)]
        public decimal Price { get; set; }
        
        [Required]
        [Column(Order = 3)]
        public int InitialStock { get; set; }
        
        [Required]
        [Column(Order = 4)]
        public ProductCategory Category { get; set; }
        
        [Required]
        [Column(Order = 5)]
        [MaxLength(255)]
        public string? ImageUrl { get; set; }
        
        public ICollection<Transaction> Transactions { get; set; }
    }
}