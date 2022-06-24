using System;

namespace Entities.DTO
{
    public class CreateBankAccountDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public decimal Balance { get; set; }
        //public int ClientId { get; set; } 

        public CreateBankAccountDTO()
        {

        }
        public CreateBankAccountDTO(string code, string name, int currencyId, decimal balance)
        {
            Code = code;
            Name = name;
            CurrencyId = currencyId;
            Balance = balance;
            
        }
    }
}
