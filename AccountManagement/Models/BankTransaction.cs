using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountManagement.Models
{
    public class BankTransaction
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("BankAccount")]
        public int BankAccountId { get; set; }
        public TransactionAction Action { get; set; } 
        public decimal Amount { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; } = DateTime.UtcNow;

        public BankAccount BankAccount { get; set; }
    }

    public enum TransactionAction
    {
        Deposit = 1,
        Withdraw = 2

    }
}
