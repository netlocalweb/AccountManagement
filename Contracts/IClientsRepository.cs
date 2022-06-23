using Entities.DTO;
using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IClientsRepository
    {
        //GETALL Method Interface
        List<GetClientDTO> GetAllRecords();


        //GETBYID Method Interface
        GetClientDTO GetRecordById(int id, out string ErrorMessage);


        //DELETE Method Interface
        void RemoveRecord(int id, out bool check);

        //CREATE Method Interface
        void CreateRecord(CreateClientDTO clientDTO, out string ErrorMessage);

        //REGISTER Method Interface
        void Register(CreateClientDTO client, out string ErrorMessage);

        //Login Method Interface
        void LoginValidation(string username, string password, out string ErrorMessage, out Clients client);

        //UPDATE Method Interface
        void UpdateRecord(int id, UpdateClientDTO clients, out string ErrorMessage);

        //SAVE Method Interface
        void SaveChanges();


    }
}