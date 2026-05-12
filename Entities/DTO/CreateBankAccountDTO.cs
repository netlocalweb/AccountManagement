using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class CreateBankAccountDTO
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public int CurrencyId { get; set; }

        public decimal Balance { get; set; }

        public int ClientId { get; set; }
    }
}