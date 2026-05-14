namespace Entities.DTO
{
    public class ClientActiveAccountReportDTO
    {
        public string AccountCode { get; set; }

        public string AccountName { get; set; }

        public string Currency { get; set; }

        public decimal CurrentBalance { get; set; }
    }
}