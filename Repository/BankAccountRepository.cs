using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class BankAccountRepository : IBankAccountRepository
    {
        protected RepositoryContext RepositoryContext;

        public BankAccountRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        //Method CREATE
        public void CreateRecord(BankAccount bankAccount, out string ErrorMessage)
        {
            if (CodeValidator(bankAccount) == true)
            {
                RepositoryContext.BankAccounts.Add(bankAccount);
                ErrorMessage = "Bank Account added to database!";
            }
            else
            {
                ErrorMessage = "User already has a bank account with this code! Record NOT added to database";
            }
            
        }
        //GETALL Method
        public IEnumerable<BankAccount> GetAllRecords()
        {
            var testAll = RepositoryContext.BankAccounts;
            return (IEnumerable<BankAccount>)testAll;
        }
        //GetRecordById Method
        public BankAccount GetRecordById(int id)
        {
            var bankAccount = RepositoryContext.BankAccounts.Where(x => x.Id == id).FirstOrDefault();
            return bankAccount;
        }
        //Remove Record Method
        public void RemoveRecord(int id)
        {
            var bankAccount = RepositoryContext.BankAccounts.Where(x => x.Id == id).FirstOrDefault();
            bankAccount.IsActive = false;
            RepositoryContext.BankAccounts.Update(bankAccount);
        }

        public void SaveChanges()
        {
            RepositoryContext.SaveChanges();
        }
        //Update Records Method
        public void UpdateRecord(int id, BankAccount bankAccount, out string ErrorMessage)
        {
            string errorMessage = string.Empty;

            var oldBankAccount = RepositoryContext.BankAccounts.Where(x => x.Id == id).FirstOrDefault();

            ErrorMessage = "Bank Account updated sucefully!";
            oldBankAccount.Code = bankAccount.Code;
            oldBankAccount.Name = bankAccount.Name;
            oldBankAccount.CurrencyId = bankAccount.CurrencyId;
            oldBankAccount.Balance = bankAccount.Balance;
            oldBankAccount.IsActive = bankAccount.IsActive;
            oldBankAccount.DateModified = DateTime.Now;
        }

        public bool CodeValidator(BankAccount newBankAccount)
        {
           
            var codeValidatior = RepositoryContext.BankAccounts.Where(x => x.Code == newBankAccount.Code && x.ClientId == newBankAccount.ClientId).FirstOrDefault();
            
            if (codeValidatior == null)
                return true;
            else
                return false;
            
        }
    }    
 }

