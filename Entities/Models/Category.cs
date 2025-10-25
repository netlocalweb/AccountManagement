namespace Entities.Models
{
    public class Category
    {
        public int Id { get; set; }
      
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }

        //navigation property for products
        public ICollection<Products> Products { get; set; } = new List<Products>();
    }
}
