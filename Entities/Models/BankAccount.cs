using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public bool IsActtive { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModefied { get; set; }


        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
