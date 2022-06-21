using Entities.DTO;
using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBankTransactionRepository
    {
        //GETALL Method Interface
        IEnumerable<BankTransaction> GetAllRecords();
        
        
        //GETBYID Method Interface
        BankTransaction GetRecordById(int id);
        
        
        //DELETE Method Interface
        void RemoveRecord(int id);
        
        //CREATE Method Interface
        void CreateRecord(BankTransaction bankTransaction, out string ErrorMessage);
        
        //SAVE Method Interface
        void SaveChanges();
    }
}