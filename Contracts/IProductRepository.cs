using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IProductRepository
    {
        //GETALL Method Interface
        IEnumerable<Product> GetAllRecords(int pageNumber, int pageSize, out int totalRecords);

        //GETBYID Method Interface
        Product GetRecordById(int id);

        //DELETE Method Interface
        void RemoveRecord(int id, out bool check);

        //CREATE Method Interface
        void CreateRecord(Product product, out string ErrorMessage);
        //Upload Image Method Interface
        void UploadImage(int id, string Image);
        //UPDATE Method Interface
        void UpdateRecord(int id, Product product, out string ErrorMessage);

        //SAVE Method Interface
        void SaveChanges();
    }
}