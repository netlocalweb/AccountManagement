using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class UpdateBankTransactionDTO
    {
        [Required]
        public int Action { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public bool IsActive { get; set; }
    }
}