using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class BankTransactionRepository : RepositoryBase<BankTransaction>, IBankTransactionRepository
    {
        public BankTransactionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        //Merr te gjitha transaksionet
        public async Task<IEnumerable<BankTransaction>> GetAllBankTransactionAsync(bool trackchanges) => 
            await FindAll(trackchanges)
            .ToListAsync();
        //Mer transaksionin sipas id
        public async Task<BankTransaction?> GetBankTransactionsByIdAsync(int id, bool trackchanges) =>
            await FindByCondition(bt => bt.Id.Equals(id), trackchanges)
            .FirstOrDefaultAsync();
        //shton nje ttransaksion te ri 
        public void CreateBankTransaction(BankTransaction bankTransaction) => Create(bankTransaction);

    }
}
