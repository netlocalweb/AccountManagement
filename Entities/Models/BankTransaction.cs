using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BankTransaction
    {
        [Key]
        public int Id { get; set; }
        public int Action { get; set; } //1,2 per Depozitim,Terheqje
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = null;

        [ForeignKey("BankAccountId")]
        public BankAccount BankAccount { get; set; }
        public int BankAccountId { get; set; }

    }
}
