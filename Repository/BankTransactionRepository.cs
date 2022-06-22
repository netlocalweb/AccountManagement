using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        protected RepositoryContext RepositoryContext;

        public BankTransactionRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        //Method CREATE
        public void CreateRecord(BankTransaction bankTransaction, out string ErrorMessage)
        {
            //action 0 = Deposit / action 1 = Withdraw
            ErrorMessage = String.Empty;
            var action = bankTransaction.Action;
            var bankAccount = RepositoryContext.BankAccounts.Where(x => x.Id == bankTransaction.BankAccountId).FirstOrDefault();
            if (bankAccount == null)
            {
                ErrorMessage = "Bank Account Not Found";
            }
            else
            {
                if (action != 0 && action != 1)
                {
                    ErrorMessage = "Input 0 for Deposit and 1 for Withdrawal";
                }
                else if (action == 0)
                {
                    Deposit(bankAccount, bankTransaction.Amount, out string errorMessage);
                    ErrorMessage = errorMessage;
                    RepositoryContext.BankTransactions.Add(bankTransaction);
                }
                else if (action == 1)
                {
                    Withdraw(bankAccount, bankTransaction.Amount, out string errorMessage, out bool check);
                    if (check == false)
                    {
                        ErrorMessage = errorMessage;
                    }
                    else
                    {
                        ErrorMessage = errorMessage;
                        RepositoryContext.BankTransactions.Add(bankTransaction);
                    }

                }
            }
        }
        //GETALL Method
        public IEnumerable<BankTransaction> GetAllRecords()
        {
            var testAll = RepositoryContext.BankTransactions;
            return (IEnumerable<BankTransaction>)testAll;
        }
        //GetRecordById Method
        public BankTransaction GetRecordById(int id)
        {
            var bankTransaction = RepositoryContext.BankTransactions.Where(x => x.Id == id).FirstOrDefault();
            return bankTransaction;
            
        }
        //Remove Record Method
        public void RemoveRecord(int id, out bool check)
        {
            var bankTransaction = RepositoryContext.BankTransactions.Where(x => x.Id == id).FirstOrDefault();
            if(bankTransaction == null)
            {
                check = false;
            }
            else
            {
                check = true;
                bankTransaction.IsActive = false;
                bankTransaction.DateModified = DateTime.Now;
                RepositoryContext.BankTransactions.Update(bankTransaction);
            }
            
        }

        public void SaveChanges()
        {
            RepositoryContext.SaveChanges();
        }
        //Withdraw method
        public void Withdraw(BankAccount bankAccount, decimal amount, out string ErrorMessage, out bool check)
        {
            var balance = bankAccount.Balance;
            if (balance < amount)
            {
                check = false;
                ErrorMessage = "You don't have enough funds to withdraw this amount";
            }
            else
            {
                bankAccount.Balance = bankAccount.Balance - amount;
                check = true;
                ErrorMessage = "Bank Account balance updated: " + bankAccount.Balance.ToString();
            }
        }

        public void Deposit(BankAccount bankAccount, decimal amount, out string ErrorMessage)
        {
            bankAccount.Balance += amount;
            ErrorMessage = "Bank Account balance updated: " + bankAccount.Balance.ToString();

        }
    }
}

