using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class UpdateBankAccountDTO
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public int CurrencyId { get; set; }

        public decimal Balance { get; set; }

        public bool IsActive { get; set; }
    }
}