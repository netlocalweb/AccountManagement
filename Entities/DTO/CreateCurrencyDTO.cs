namespace Entities.DTO
{
    public class CreateCurrencyDTO
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal ExchangeRate { get; set; }

    }
}
