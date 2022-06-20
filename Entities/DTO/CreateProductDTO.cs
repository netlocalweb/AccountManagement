using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class CreateProductDTO
    {

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public decimal Price { get; set; }
        

        public DateTime DateCreated { get; set; }

        public CreateProductDTO(string name, string shortDescription, string longDescription,int categoryId, decimal price)
        {
            Name = name;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            CategoryId = categoryId;
            Price = price;
            DateCreated = DateTime.Now;
        }


    }
}
