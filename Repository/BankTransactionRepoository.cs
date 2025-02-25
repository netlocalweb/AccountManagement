using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Delete(int id)
        {
            var bankTransaction = _repositoryContext.BankTransactions.Find(id);
            if (bankTransaction != null)
            {
                _repositoryContext.BankTransactions.Remove(bankTransaction);
                _repositoryContext.SaveChanges();
            }
        }

        public ICollection<BankTransaction> FindAll()
        {
            return _repositoryContext.BankTransactions.ToList();
        }

        public BankTransaction FindById(int id)
        {
            return _repositoryContext.BankTransactions.Find(id);
        }

        public bool Update(BankTransaction entity)
        {
            var existingBankTransaction = _repositoryContext.BankTransactions.Find(entity.Id);

            // Update the DateModified
            existingBankTransaction.DateModified = DateTime.Now;

            //whatever to-change properties do i shof me von

            // Save the changes
            _repositoryContext.SaveChanges();
            return true;
        }

        public void SoftDelete(BankTransaction bankTransaction)
        {
            bankTransaction.IsActive = false;
            _repositoryContext.Update(bankTransaction);
            _repositoryContext.SaveChanges();
        }

    }
}
