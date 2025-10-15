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
    internal sealed class BankTransactionRepository : RepositoryBase<BankTransaction>, IBankTransactionRepository
    {
        public BankTransactionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<BankTransaction>> GetAllBankTransactionAsync(bool trackchanges) => 
            await FindAll(trackchanges)
            .ToListAsync();
        public async Task<BankTransaction?> GetBankTransactionsByIdAsync(int id, bool trackchanges) =>
            await FindByCondition(bt => bt.Id.Equals(id), trackchanges)
            .FirstOrDefaultAsync();

        public void CreateBankTransaction(BankTransaction bankTransaction) => Create(bankTransaction);
        public void UpdateBankTransaction(BankTransaction bankTransactionn) => Update(bankTransactionn);
        public void DeleteBankTransaction(BankTransaction bankTransaction) => Delete(bankTransaction);
    }
}
