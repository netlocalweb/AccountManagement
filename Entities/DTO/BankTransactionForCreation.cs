using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class BankTransactionForCreation
    {
        public int BankAccountId { get; set; }
        public string BankAccountCode { get; set; }
        public TransactionAction Action { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
