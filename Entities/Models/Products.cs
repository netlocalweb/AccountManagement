using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Products
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        
        public string ShortDescription { get; set; }
        [Required]
        public string  LongDescription { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? ImagePath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }


        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        //navigation prop
        public Category Category { get; set; }

    }
}
