using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class BankTransactionForUpdate
    {
        public TransactionAction Action { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
