using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAllCurrenciesAsync();

        Task<Currency> GetCurrencyByIdAsync(int id);

        Task<Currency> GetCurrencyByCodeAsync(string code);

        void CreateCurrency(Currency currency);

        void UpdateCurrency(Currency currency);

        void DeleteCurrency(Currency currency);
    }
}