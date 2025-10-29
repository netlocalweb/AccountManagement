using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetAllAsync(int clientId);
        Task<BankAccount> GetByIdAsync(int id);
        Task<BankAccount> AddAsync(BankAccount account);
        Task<BankAccount> UpdateAsync(BankAccount account);
        Task<bool> SoftDeleteAsync(int id);
        Task<bool> ExistsByCodeAsync(int clientId, string code, int? excludeId = null);

    }
}
