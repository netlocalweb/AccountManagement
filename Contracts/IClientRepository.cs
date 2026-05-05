using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> GetClientByEmailAsync(string email);
        Task<Client> GetClientByPhoneAsync(string phone);
        Task<Client> GetClientByUsernameAsync(string username);

        void CreateClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
    }
}