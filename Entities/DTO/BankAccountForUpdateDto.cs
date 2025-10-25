using System.Text.Json.Serialization;

namespace Entities.DTO
{
    public class BankAccountForUpdateDto
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public int CurrencyId { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public DateTime? DateModified { get; set; } = DateTime.Now;

    }
}
