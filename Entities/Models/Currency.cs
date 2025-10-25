using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;

        [Column(TypeName ="decimal(18,4)")]
        public decimal ExchangeRate { get; set; }


        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }

        //navigation propert for bank acc 
        public ICollection<BankAccount>? BankAccounts { get; set; }

    }
}
