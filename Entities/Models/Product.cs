using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter short description")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Enter long description")]
        public string LongDescription { get; set; }
        [Required(ErrorMessage = "Enter category id")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [Required(ErrorMessage = "Enter price")]
        public decimal Price { get; set; }
        public string Image { get; set; }

        public DateTime DateCreated { get; set; }
        
        public DateTime? DateModified { get; set; }


        public Product() { }

        public Product(string name, string shortDescription, string longDescription, int CategoryId, decimal price)
        {

            this.Name = name;
            this.ShortDescription = shortDescription;
            this.LongDescription = longDescription;
            this.CategoryId = CategoryId;
            this.Price = price;
            this.DateCreated = DateTime.Now;


        }


    }
}
