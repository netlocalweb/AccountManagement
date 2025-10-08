using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class CurrencyUpdateDto
    {
        [Required(ErrorMessage = "Code is required.")]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Desccription { get; set; }

        [Required(ErrorMessage = "Exchange rate is required.")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "Exchange rate must be greater than zero.")]
        public decimal ExchangeRate { get; set; }
    }
}
