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
    public class BankTransactionDTO
    {
        public int Id { get; set; }
        [Required]
        public int Action { get; set; } //1,2 per Depozitim,Terheqje
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; } 
        public DateTime? DateModified { get; set; } 
        public int BankAccountId { get; set; }

    }
}
