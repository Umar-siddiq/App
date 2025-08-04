using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Utility.Shared
{
    public class ProductDto
    {
        
        public int ProductId { get; set; }
        [Required, MaxLength(255)]
        public string ProductName { get; set; }
        [Required]
        public decimal ListPrice { get; set; }

    }
}
