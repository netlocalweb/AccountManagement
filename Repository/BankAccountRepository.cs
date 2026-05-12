using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public BankAccountRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IEnumerable<BankAccount> GetBankAccounts()
        {
            return _repositoryContext.BankAccounts
                .OrderByDescending(b => b.DateCreated)
                .ToList();
        }

        public BankAccount GetBankAccountById(int id)
        {
            return _repositoryContext.BankAccounts
                .FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<BankAccount> GetBankAccountsByClientId(int clientId)
        {
            return _repositoryContext.BankAccounts
                .Where(b => b.ClientId == clientId)
                .OrderByDescending(b => b.DateCreated)
                .ToList();
        }

        public BankAccount GetBankAccountByCodeAndClientId(string code, int clientId)
        {
            return _repositoryContext.BankAccounts
                .FirstOrDefault(b => b.Code == code && b.ClientId == clientId);
        }

        public void CreateBankAccount(BankAccount bankAccount)
        {
            _repositoryContext.BankAccounts.Add(bankAccount);
        }

        public void UpdateBankAccount(BankAccount bankAccount)
        {
            _repositoryContext.BankAccounts.Update(bankAccount);
        }

        public void DeleteBankAccount(BankAccount bankAccount)
        {
            _repositoryContext.BankAccounts.Remove(bankAccount);
        }
    }
}