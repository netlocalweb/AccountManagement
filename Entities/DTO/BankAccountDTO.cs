using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

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

    public class SelectBankAccDTO //for when the client wants to login to an existing bank acc
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
