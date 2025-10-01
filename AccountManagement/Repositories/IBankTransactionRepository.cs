using AccountManagement.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Repositories
{
    public interface IBankTransactionRepository
    {
        Task<IEnumerable<BankTransaction>> GetAllAsync();
        Task<BankTransaction> GetByIdAsync(int id);
        Task<BankTransaction> AddAsync(BankTransaction transaction);
    }
}
