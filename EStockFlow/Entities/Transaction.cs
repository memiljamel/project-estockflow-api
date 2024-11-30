using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EStockFlow.Enums;

namespace EStockFlow.Entities
{
    [Table("Transactions")]
    public class Transaction : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        public int Quantity { get; set; }
        
        [Required]
        [Column(Order = 2)]
        public ProductCategory Category { get; set; }
        
        [Required]
        [Column(Order = 3)]
        public decimal Price { get; set; }
        
        [Column(Order = 4)]
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        
        public Product Product { get; set; }
    }
}