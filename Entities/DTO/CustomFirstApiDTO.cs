namespace Entities.DTO
{
    public class CustomFirstApiDTO
    {
        /*
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
        */
        //public string ClientCode { get; set; }

        public string ClientName { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string Currency { get; set; }
        public decimal CurrentBalance { get; set; }

    }
}
