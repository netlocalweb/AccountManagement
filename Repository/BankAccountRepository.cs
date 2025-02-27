using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;

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

            //whatever to-change properties do i shof me von
            existingBankAccount.DateModified = DateTime.Now;
            existingBankAccount.Balance = entity.Balance;

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

        public List<BankAccount> FindByClientId(int id)
        {
            return _repositoryContext.BankAccounts
                .Where(t => t.ClientId == id 
                        && t.IsActive == true)
                .Select(t => new BankAccount
                {
                    Code = t.Name,   
                    Name = t.Id.ToString(), 
                    Balance = t.Balance,
                    CurrencyId = t.CurrencyId
                })
                .ToList();
        }

    }
}
