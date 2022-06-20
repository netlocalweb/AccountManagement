using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Contracts
{
    public interface IProductRepository
    {
        //GETALL Method Interface
        IEnumerable<Product> GetAllRecords();
        
        //GETBYID Method Interface
        Product GetRecordById(int id);
        
        //DELETE Method Interface
        void RemoveRecord(int id);
        
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