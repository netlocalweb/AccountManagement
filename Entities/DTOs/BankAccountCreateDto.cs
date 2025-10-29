using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class BankAccountCreateDto
    {
        [Required]
        public string Code { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}
