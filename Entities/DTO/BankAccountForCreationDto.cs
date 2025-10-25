using System.Text.Json.Serialization;

namespace Entities.DTO
{
    public class BankAccountForCreationDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public decimal Balance { get; set; }

        [JsonIgnore]
        public int ClientId { get; set; }

    }

}
