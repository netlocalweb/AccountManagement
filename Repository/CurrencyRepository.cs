using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed  class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        public CurrencyRepository( RepositoryContext repositoryContext) : base(repositoryContext ) 
        {
        }
        //Merr te gjitha currency 
        public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Code)
            .ToListAsync();
        //Merr currency sipas id 
        public async Task<Currency> GetCurrencyByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id == id, trackChanges)
            .SingleOrDefaultAsync();

        //Shton nje currency te re
        public void CreateCurrency(Currency currency) => Create(currency);
        //fshin nje currency
        public void DeleteCurrency(Currency currency) => Delete(currency);
        //Update nje currency ekzistuese
        public void UpdateCurrency(Currency currency) => Update(currency);
    }
}
