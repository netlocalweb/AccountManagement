using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class BankAccountUpdateDto
    {
        [Required]
        public string Code { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public decimal? Balance { get; set; }
    }
}
