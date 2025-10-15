using AccountManagement.Models;
using AccountManagement.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Repositories
{
    public interface IBankTransactionRepository
    {
        Task<IEnumerable<BankTransaction>> GetAllAsync(int bankAccountId);
        Task<BankTransaction> GetByIdAsync(int id);
        Task<BankTransaction> AddAsync(BankTransaction transaction);
        Task<bool> SoftDeleteAsync(int id);
    }
}
