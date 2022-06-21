using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class CreateBankAccountDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }

        public CreateBankAccountDTO(string code, string name, int currencyId, decimal balance, int clientId, bool isActive)
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
