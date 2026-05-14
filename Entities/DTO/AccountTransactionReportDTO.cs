using System;

namespace Entities.DTO
{
    public class AccountTransactionReportDTO
    {
        public string Action { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}