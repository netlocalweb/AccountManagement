using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync()
        {
            return await RepositoryContext.Currencies
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        public async Task<Currency> GetCurrencyByIdAsync(int id)
        {
            return await RepositoryContext.Currencies
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Currency> GetCurrencyByCodeAsync(string code)
        {
            return await RepositoryContext.Currencies
                .FirstOrDefaultAsync(c => c.Code == code);
        }

        public void CreateCurrency(Currency currency)
        {
            RepositoryContext.Currencies.Add(currency);
        }

        public void UpdateCurrency(Currency currency)
        {
            RepositoryContext.Currencies.Update(currency);
        }

        public void DeleteCurrency(Currency currency)
        {
            RepositoryContext.Currencies.Remove(currency);
        }
    }
}