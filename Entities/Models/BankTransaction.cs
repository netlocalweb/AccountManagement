using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BankTransaction
    {
        public int  Id{ get; set; }

        [Required]
        public int BankAccountId{ get; set; }
        public BankAccount BankAccount { get; set; } = null!;

        [Required]
        public TransactionAction Action { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Shuma duhet te jete me e madhe se 0.")]
        public decimal Amount { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }

    }
}
