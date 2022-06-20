using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "Enter the code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Enter a description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter the exchange rate")]
        public decimal ExchangeRate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public Currency()
        {

        }

        public Currency(string code, string description, decimal exchangeRate)
        {
            Code = code.ToUpper();
            Description = description;
            ExchangeRate = exchangeRate;
            DateCreated = DateTime.Now;
           
            
        }

    }
}
