using System;

namespace AccountManagement.API.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public TransactionAction Action { get; set; } 
        public decimal Amount { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }

        public BankAccount BankAccount { get; set; }
    }

    public enum TransactionAction
    {
        Deposit = 0,
        Withdraw = 1

    }
}
