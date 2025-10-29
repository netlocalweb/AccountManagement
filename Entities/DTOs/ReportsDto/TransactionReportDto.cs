using System;

namespace Entities.DTOs.ReportsDto
{
    public class TransactionReportDto
    {
        public string Action { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

    }
}
