using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class BankTransactionCreateDto
    {
        [Required]
        public int BankAccountId { get; set; }

        [Required]
        public TransactionAction Action { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
