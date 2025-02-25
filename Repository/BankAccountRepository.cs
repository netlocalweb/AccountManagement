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
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public BankAccountRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Create(BankAccount bankAccount)
        {
            _repositoryContext.BankAccounts.Add(bankAccount);
            _repositoryContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var bankAccount = _repositoryContext.BankAccounts.Find(id);
            if (bankAccount != null)
            {
                _repositoryContext.BankAccounts.Remove(bankAccount);
                _repositoryContext.SaveChanges();
            }
        }

        public ICollection<BankAccount> FindAll()
        {
            return _repositoryContext.BankAccounts.ToList();
        }

        public BankAccount FindById(int id)
        {
            return _repositoryContext.BankAccounts.Find(id);
        }

        public bool Update(BankAccount entity)
        {
            var existingBankAccount = _repositoryContext.BankAccounts.Find(entity.Id);

            // Update the DateModified
            existingBankAccount.DateModified = DateTime.Now;

            //whatever to-change properties do i shof me von

            // Save the changes
            _repositoryContext.SaveChanges();
            return true;
        }

        public void SoftDelete(BankAccount bankAccount)
        {
            bankAccount.IsActive = false;
            _repositoryContext.Update(bankAccount);
            _repositoryContext.SaveChanges();
        }

    }
}
