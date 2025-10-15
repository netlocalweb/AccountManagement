
using AccountManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
        Task<Client> GetByEmailAsync(string email);
        Task<Client> GetByUsernameAsync(string username);
        Task<Client> GetByPhoneAsync(string phone);
        Task<Client> CreateAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Client client);

       
        Task<Client> AddAsync(Client client);

    }
}
