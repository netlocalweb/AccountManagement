using AccountManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Repositories
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAllAsync();
        Task<Currency> GetByIdAsync(int id);

        Task<Currency> GetByCodeAsync(string code);
        Task<Currency> AddAsync(Currency currency);
        Task<Currency> UpdateAsync(Currency currency);
        Task<bool> DeleteAsync(int id);
    }
}
