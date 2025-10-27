using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Reports
{
    public class CategoryProductsDto
    {
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; } 
    }
}
