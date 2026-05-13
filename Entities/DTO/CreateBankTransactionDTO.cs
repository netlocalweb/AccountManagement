using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class CreateBankTransactionDTO
    {
        [Required]
        public int BankAccountId { get; set; }

        [Required]
        public int Action { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}