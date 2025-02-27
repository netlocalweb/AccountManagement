using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public BankTransactionRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Create(BankTransaction bankTransaction)
        {
            _repositoryContext.BankTransactions.Add(bankTransaction);
            _repositoryContext.SaveChanges();
        }

        public ICollection<BankTransaction> FindAll()
        {
            return _repositoryContext.BankTransactions.ToList();
        }

        public BankTransaction FindById(int id)
        {
            return _repositoryContext.BankTransactions.Find(id);
        }

        public List<BankTransaction> FindByBankAccountId(int id)
        {
            return _repositoryContext.BankTransactions
                .Where(t => t.BankAccountId == id)
                .Select(t => new BankTransaction
                {
                    Action = t.Action,
                    Amount = t.Amount,
                    DateCreated = t.DateCreated
                })
                .ToList();
        }

        public bool Update(BankTransaction entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
