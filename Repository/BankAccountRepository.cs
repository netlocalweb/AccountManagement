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
            .Include(b => b.Client)
            .Include(b => b.Currency)
            .ToListAsync();

        public async Task<BankAccount?> GetBankAccountsByIdAsync(int id, bool trackchanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackchanges)
            .Include(b => b.Client)
            .Include(b => b.Currency)
            .SingleOrDefaultAsync();

        public void CreateBankAccunt(BankAccount bankAccount) => Create(bankAccount);
        public void UpdateBankAccount(BankAccount bankAccount) => Update(bankAccount);
    }
}
