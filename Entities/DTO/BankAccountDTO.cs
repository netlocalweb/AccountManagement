using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class BankAccountDTO
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Balance { get; set; } = 0;
        [Required]
        public bool IsActive { get; set; } = true;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int CurrencyId { get; set; }
        public int ClientId { get; set; }
    }

    public class CreateBankAccDTO
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public int ClientId { get; set; }
    }

}
