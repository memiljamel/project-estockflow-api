using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EStockFlow.Enums;

namespace EStockFlow.Entities
{
    [Table("Transactions")]
    public class Transaction : BaseEntity
    {
        [Column(Order = 1)]
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        
        [Required]
        [Column(Order = 2)]
        public int Quantity { get; set; }
        
        [Required]
        [Column(Order = 3)]
        public ProductCategoryEnum Category { get; set; }
        
        [Required]
        [Column(Order = 4)]
        public decimal Price { get; set; }
        
        [Required]
        [Column(Order = 5)]
        public decimal Amount { get; set; }
        
        public Product Product { get; set; }
    }
}