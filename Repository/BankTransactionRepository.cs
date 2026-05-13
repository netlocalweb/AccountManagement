using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public BankTransactionRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IEnumerable<BankTransaction> GetBankTransactions()
        {
            return _repositoryContext.BankTransactions
                .OrderByDescending(t => t.DateCreated)
                .ToList();
        }

        public BankTransaction GetBankTransactionById(int id)
        {
            return _repositoryContext.BankTransactions
                .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<BankTransaction> GetBankTransactionsByBankAccountId(int bankAccountId)
        {
            return _repositoryContext.BankTransactions
                .Where(t => t.BankAccountId == bankAccountId)
                .OrderByDescending(t => t.DateCreated)
                .ToList();
        }

        public void CreateBankTransaction(BankTransaction bankTransaction)
        {
            _repositoryContext.BankTransactions.Add(bankTransaction);
        }

        public void UpdateBankTransaction(BankTransaction bankTransaction)
        {
            _repositoryContext.BankTransactions.Update(bankTransaction);
        }

        public void DeleteBankTransaction(BankTransaction bankTransaction)
        {
            _repositoryContext.BankTransactions.Remove(bankTransaction);
        }
    }
}