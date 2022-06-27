using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBankAccountRepository
    {
        //GETALL Method Interface
        IEnumerable<BankAccount> GetAllRecords(int pageNumber, int pageSize, out int totalRecords);


        //GETBYID Method Interface
        BankAccount GetRecordById(int id, out int validation);



        //Remove Method Interface
        void RemoveRecord(int id, out bool check);

        //CREATE Method Interface
        void CreateRecord(BankAccount bankAccount, out string ErrorMessage);

        //UPDATE Method Interface
        void UpdateRecord(int id, BankAccount bankAccount, out string ErrorMessage);

        //SAVE Method Interface
        void SaveChanges();
    }
}