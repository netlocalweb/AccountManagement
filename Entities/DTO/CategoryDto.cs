namespace Entities.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }
    }
}
