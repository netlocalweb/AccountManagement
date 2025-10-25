using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ClientReports
    {
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public string Currendy { get; set; }
        public decimal CurrentBalance { get; set; }
    }

    public class AccountTransaction
    {
        public string Action { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } 
    }

    public class ClientAccounts
    {
        public string AccountCode{ get; set; }
        public string AccountName { get; set; }
        public string Currency{ get; set; }
        public decimal CurrentBalance { get; set; }

    }

    public class CategoryPoducts
    {
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
