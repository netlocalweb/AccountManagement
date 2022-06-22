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
            var objCurrency = RepositoryContext.Currencies.Where(x => x.Id == bankAccount.CurrencyId).FirstOrDefault();
            var objClient = RepositoryContext.Clients.Where(x => x.Id == bankAccount.ClientId).FirstOrDefault();
            if (objCurrency == null)
            {
                ErrorMessage = "Currency Id not found";
            }
            else if (objClient == null)
            {
                ErrorMessage = "Client Id not found";
            }
            else if (CodeValidator(bankAccount) == true)
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
        public void RemoveRecord(int id, out bool check)
        {
            var bankAccount = RepositoryContext.BankAccounts.Where(x => x.Id == id).FirstOrDefault();
            if(bankAccount == null)
            {
                check = false;
            }
            else
            {
                check = true;
                bankAccount.IsActive = false;
                bankAccount.DateModified = DateTime.Now;
                RepositoryContext.BankAccounts.Update(bankAccount);
            }
            
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
            var objCurrency = RepositoryContext.Currencies.Where(x => x.Id == bankAccount.CurrencyId).FirstOrDefault();
            var objClient = RepositoryContext.Clients.Where(x => x.Id == bankAccount.ClientId).FirstOrDefault();
            if(oldBankAccount == null)
            {
                ErrorMessage = "There is no Bank Account with this ID in Database";
            }else if (objCurrency == null)
            {
                ErrorMessage = "Currency Id not found";
            }
            else if (objClient == null)
            {
                ErrorMessage = "Client Id not found";
            }
            else
            {
                ErrorMessage = "Bank Account updated sucefully!";
                oldBankAccount.Code = bankAccount.Code;
                oldBankAccount.Name = bankAccount.Name;
                oldBankAccount.CurrencyId = bankAccount.CurrencyId;
                oldBankAccount.Balance = bankAccount.Balance;
                oldBankAccount.IsActive = bankAccount.IsActive;
                oldBankAccount.DateModified = DateTime.Now;
            }
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

