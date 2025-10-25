using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync(bool trackChanges);
        Task<Client?> GetClientByIdAsync(int id ,bool trackChanges);
        Task<Client> GetClientByUserIdAsync(string userId ,bool trackChanges);
        void CreateClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);

    }
}
