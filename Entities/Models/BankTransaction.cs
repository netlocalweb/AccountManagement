using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class BankTransaction
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Bank Account Id")]
        public int BankAccountId { get; set; }
        [ForeignKey("BankAccountId")]
        public BankAccount BankAccount { get; set; }
        [Required(ErrorMessage = "Enter Action")]
        public int Action { get; set; }
        [Required(ErrorMessage = "Enter Amount")]
        public decimal Amount { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public BankTransaction() { }

        public BankTransaction(int bankAccountId, int action, decimal amount, bool isActive)
        {

            BankAccountId = bankAccountId;
            Action = action;
            Amount = amount;
            IsActive = isActive;
            DateCreated = DateTime.Now;

        }
    }
}
