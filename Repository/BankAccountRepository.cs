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

        public async Task<IEnumerable<BankAccount>> GetAllBAnkAccountsAsync(bool trackchanges) =>
            await FindAll(trackchanges)
            .Where(b => b.IsActtive)
            .Include(b => b.Client)
            .Include(b => b.Currency)
            .ToListAsync();

        public async Task<BankAccount?> GetBankAccountsByIdAsync(int id, bool trackchanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackchanges)
            .Include(b => b.Client)
            .Include(b => b.Currency)
            .FirstOrDefaultAsync( b => b.Id == id && b.IsActtive);

        public void UpdateBankAccount(BankAccount bankAccount) => Update(bankAccount);

        public void CreateBankAccount(BankAccount bankAccount) => Create(bankAccount);

        public async Task<bool> CodeExistsForClientAsync(string code, int clientId)
        {
            return await FindByCondition(b => b.Code == code && b.ClientId == clientId, false)
                .AnyAsync(b => b.IsActtive);
        }
    }
}
