using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBankTransactionRepository
    {
        //GETALL Method Interface
        IEnumerable<BankTransaction> GetAllRecords(int pageNumber, int pageSize, out int totalRecords);


        //GETBYID Method Interface
        BankTransaction GetRecordById(int id);


        //Remove Method Interface
        void RemoveRecord(int id, out bool check);

        //CREATE Method Interface
        void CreateRecord(BankTransaction bankTransaction, out string ErrorMessage);

        //SAVE Method Interface
        void SaveChanges();
    }
}