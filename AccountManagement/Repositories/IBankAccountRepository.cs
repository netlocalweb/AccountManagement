using AccountManagement.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.API.Repositories
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetAllAsync(int clientId);
        Task<BankAccount> GetByIdAsync(int id);
        Task<BankAccount> AddAsync(BankAccount account);
        Task<BankAccount> UpdateAsync(BankAccount account);
    }
}
