using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IClientsRepository
    {
        //GETALL Method Interface
        IEnumerable<Clients> GetAllRecords();


        //GETBYID Method Interface
        Clients GetRecordById(int id);


        //DELETE Method Interface
        void RemoveRecord(int id, out bool check);

        //CREATE Method Interface
        void CreateRecord(Clients client, out string ErrorMessage);

        //REGISTER Method Interface
        void Register(Clients client, out string ErrorMessage);

        //Login Method Interface
        void LoginValidation(string username, string password, out string ErrorMessage, out Clients client);

        //UPDATE Method Interface
        void UpdateRecord(int id, Clients clients, out string ErrorMessage);

        //SAVE Method Interface
        void SaveChanges();


    }
}