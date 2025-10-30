using Entities.Models;

namespace Contracts
{
    public interface IBankTransactionRepository : IRepositoryBase<BankTransaction>
    {
        Task<IEnumerable<BankTransaction>> GetAllBankTransactionAsync(bool trackchanges);
        Task<BankTransaction?> GetBankTransactionsByIdAsync(int id, bool trackchanges);
        void CreateBankTransaction(BankTransaction bankTransaction);

    }
}
