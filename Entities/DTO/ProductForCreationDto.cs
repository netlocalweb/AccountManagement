using Microsoft.AspNetCore.Http;

namespace Entities.DTO
{
    public class ProductForCreationDto
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? ImagePath { get; set; }
    }
}
