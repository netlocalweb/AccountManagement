using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Reports
{
    public class AccountTransactionDto
    {
        public string Action { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
