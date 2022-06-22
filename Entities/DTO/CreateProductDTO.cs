using System;
using System.ComponentModel.DataAnnotations.Schema;

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


        

        


    }
}
