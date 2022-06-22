using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class CreateBankTransactionDTO
    {

        public int BankAccountId { get; set; }

        [Required(ErrorMessage = "Enter Action")]
        public int Action { get; set; }
        [Required(ErrorMessage = "Enter Amount")]
        public decimal Amount { get; set; }
        
       


    }
}
