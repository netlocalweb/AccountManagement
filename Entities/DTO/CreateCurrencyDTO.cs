using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class CreateCurrencyDTO
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "999999999999", ErrorMessage = "ExchangeRate must be greater than 0.")]
        public decimal ExchangeRate { get; set; }
    }
}