using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        //Merr llogarite aktive me inormacionin e klientit dhe monedhes 
        public async Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync(bool trackchanges) =>
            await FindAll(trackchanges)
            .Where(b => b.IsActive)
            .Include(b => b.Client)
            .Include(b => b.Currency)
            .ToListAsync();
        //Merr nje llogari sipas id
        public async Task<BankAccount?> GetBankAccountByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id == id && b.IsActive, trackChanges)
                .Include(b => b.Client)
                .Include(b => b.Currency)
                .FirstOrDefaultAsync();


        public void UpdateBankAccount(BankAccount bankAccount) => Update(bankAccount);

        public void CreateBankAccount(BankAccount bankAccount) => Create(bankAccount);
        //Kontrollon nqs ekziston nje bank acc per nje klient 
        public async Task<bool> CodeExistsForClientAsync(string code, int clientId)
        {
            return await FindByCondition(b => b.Code == code && b.ClientId == clientId, false)
                .AnyAsync(b => b.IsActive);
        }
        //Merr llogarinte ektive per nje klient te caktuar 
        public async Task<IEnumerable<BankAccount>> GetBankAccountByClientId(int clientId, bool trackchanges)
        {
            return await FindByCondition(b => b.ClientId == clientId && b.IsActive, trackchanges)
                .Include(b => b.Client)
                .Include(b => b.Currency)
                .ToListAsync();
        }
    }
}
