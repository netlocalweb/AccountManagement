using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class CurrencyDTO
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal ExchangeRate { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }

    public class AddCurrencyDTO
    {
        public int Id { get; set; } 
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal ExchangeRate { get; set; }
    }
}
