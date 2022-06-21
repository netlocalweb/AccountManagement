using Entities.DTO;
using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBankAccountRepository
    {
        //GETALL Method Interface
        IEnumerable<BankAccount> GetAllRecords();
        
        
        //GETBYID Method Interface
        BankAccount GetRecordById(int id);
        
        
        //DELETE Method Interface
        void RemoveRecord(int id);
        
        //CREATE Method Interface
        void CreateRecord(BankAccount bankAccount, out string ErrorMessage);
        
        //UPDATE Method Interface
        void UpdateRecord(int id, BankAccount bankAccount, out string ErrorMessage);
        
        //SAVE Method Interface
        void SaveChanges();
    }
}