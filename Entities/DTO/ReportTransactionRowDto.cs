using System;

namespace Entities.DTO
{
    public class ReportTransactionRowDto
    {
        public string Action { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
