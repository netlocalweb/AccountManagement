using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface ICurrencyRepository
    {
        //GETALL Method Interface
        IEnumerable<Currency> GetAllRecords(int pageNumber, int pageSize, out int totalRecords);


        //GETBYID Method Interface
        Currency GetRecordById(int id);


        //DELETE Method Interface
        void RemoveRecord(int id, out bool check);

        //CREATE Method Interface
        void CreateRecord(Currency currency, out string ErrorMessage);

        //UPDATE Method Interface
        void UpdateRecord(int id, Currency currency, out string ErrorMessage);

        //SAVE Method Interface
        void SaveChanges();
    }
}