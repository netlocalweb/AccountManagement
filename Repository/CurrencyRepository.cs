using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal sealed  class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository( RepositoryContext repositoryContext) : base(repositoryContext ) 
        {
        }
        public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Code)
            .ToListAsync();
        
        public async Task<Currency> GetCurrencyByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id == id, trackChanges)
            .SingleOrDefaultAsync();


        public void CreateCurrency(Currency currency) => Create(currency);
        public void DeleteCurrency(Currency currency) => Delete(currency);

        public void UpdateCurrency(Currency currency) => Update(currency);
    }
}
