namespace Entities.DTOs
{
    public class CurrencyCreateDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
