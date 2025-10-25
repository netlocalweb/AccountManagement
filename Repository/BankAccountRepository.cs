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
    internal sealed class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync(bool trackchanges) =>
            await FindAll(trackchanges)
            .Where(b => b.IsActive)
            .Include(b => b.Client)
            .Include(b => b.Currency)
            .ToListAsync();

        public async Task<BankAccount?> GetBankAccountByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id == id && b.IsActive, trackChanges)
                .Include(b => b.Client)
                .Include(b => b.Currency)
                .FirstOrDefaultAsync();


        public void UpdateBankAccount(BankAccount bankAccount) => Update(bankAccount);

        public void CreateBankAccount(BankAccount bankAccount) => Create(bankAccount);

        public async Task<bool> CodeExistsForClientAsync(string code, int clientId)
        {
            return await FindByCondition(b => b.Code == code && b.ClientId == clientId, false)
                .AnyAsync(b => b.IsActive);
        }
    }
}
