namespace Entities.DTO.Reports
{
    public class ClientAccountDto
    {
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string Currency  { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
