using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface ICategoryRepository
    {
        //GETALL Method Interface
        IEnumerable<Category> GetAllRecords(int pageNumber, int pageSize, out int totalRecords);


        //GETBYID Method Interface
        Category GetRecordById(int id);


        //DELETE Method Interface
        void RemoveRecord(int id, out bool check);

        //CREATE Method Interface
        void CreateRecord(Category category, out string ErrorMessage);

        //UPDATE Method Interface
        void UpdateRecord(int id, Category category, out string ErrorMessage);

        //SAVE Method Interface
        void SaveChanges();
    }
}