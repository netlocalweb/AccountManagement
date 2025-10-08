using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAllCurrenciesAsync(bool trackChanges);
        Task<Currency> GetCurrencyByIdAsync(int id, bool trackChanges);
        Task<Currency> GetCurrencyByCodeAsync(string code, bool trackChanges);
        void CreateCurrency(Currency currency);
        void DeleteCurrency(Currency currency);

    }
}
