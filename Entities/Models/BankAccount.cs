using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public bool IsActtive { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public DateTime DateModefied { get; set; }


        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        //navigation properties
        public ICollection<BankTransaction> BankTransactions { get; set; } = new List<BankTransaction>();
    }
}
