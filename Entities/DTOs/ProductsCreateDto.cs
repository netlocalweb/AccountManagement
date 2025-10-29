using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class ProductsCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string ShortDescription { get; set; } = null!;

        [Required]
        public string LongDescription { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IFormFile? Image { get; set; }
    }
}
