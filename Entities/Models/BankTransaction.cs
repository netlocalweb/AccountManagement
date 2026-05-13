using System;

namespace Entities.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }

        public int BankAccountId { get; set; }

        public int Action { get; set; }

        public decimal Amount { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}