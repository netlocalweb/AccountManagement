using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Enter description")]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public Category()
        {

        }

        public Category(string code, string description)
        {
           
            Code = code.ToUpper();
            Description = description;
            DateCreated = DateTime.Now;
            
        }
    }
}
