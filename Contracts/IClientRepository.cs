using Entities.Models;

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
