using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IAuthRepository
    {
        //GETALL Method Interface
        IEnumerable<Clients> GetAllRecords();


        //GETBYID Method Interface
        Clients GetRecordById(int id);


        //DELETE Method Interface
        void RemoveRecord(int id);

        //CREATE Method Interface
        void CreateRecord(Clients client, out string ErrorMessage);

        //Update Method Interface
        void UpdateRecord(int id, Clients clients, out string ErrorMessage);

        //Login Validation Method Interface
        void LoginValidation(string username, string password, out string ErrorMessage);

        //SAVE Method Interface
        void SaveChanges();


    }
}