using System;

namespace Entities.DTO
{
    public class CreateBankAccountDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
       

        
    }
}
