using Entities.Models;
using System;

namespace Entities.DTOs
{
    public class BankTransactionReadDto
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public TransactionAction Action { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }

}

