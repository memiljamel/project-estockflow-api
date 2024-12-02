using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EStockFlow.Entities
{
    [Table("Users")]
    [Index(nameof(Username), IsUnique = true)]
    public class User : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        [MaxLength(100)]
        public string? Name { get; set; }
        
        [Required]
        [Column(Order = 2)]
        [MaxLength(100)]
        public string? Username { get; set; }
        
        [Required]
        [Column(Order = 3)]
        [MaxLength(255)]
        public string? Password { get; set; }
    }
}