using Entities.Models;

namespace Contracts
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAllCurrenciesAsync(bool trackChanges);
        Task<Currency> GetCurrencyByIdAsync(int id, bool trackChanges);
        void CreateCurrency(Currency currency);
        void DeleteCurrency(Currency currency);
        void UpdateCurrency(Currency currency);

    }
}
