using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter CurrencyId")]
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        [Required(ErrorMessage = "Enter balance")]
        public decimal Balance { get; set; } = 0;
        [Required(ErrorMessage = "Enter ClientId")]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Clients Clients { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public BankAccount() { }

        public BankAccount(string code, string name, int currencyId, decimal balance, int clientId)
        {
            Code = code;
            Name = name;
            CurrencyId = currencyId;
            Balance = balance;
            ClientId = clientId;
            IsActive = true;
            DateCreated = DateTime.Now;
        }

        


    }
}
