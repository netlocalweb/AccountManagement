namespace Entities.DTO
{
    public class ProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public string? ImagePath { get; set; }

    }
}
