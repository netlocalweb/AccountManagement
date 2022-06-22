using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        protected RepositoryContext RepositoryContext;

        public CurrencyRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        //Method CREATE
        public void CreateRecord(Currency currency, out string ErrorMessage)
        {
            string errorMessage = string.Empty;
            if (DuplicateValidation(currency, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;
            }
            else
            {
                ErrorMessage = "Currency added to database.";
                RepositoryContext.Currencies.Add(currency);
            }

        }



        //Method GETALL
        public IEnumerable<Currency> GetAllRecords()
        {
            var testAll = RepositoryContext.Currencies;
            return (IEnumerable<Currency>)testAll;

        }
        //Method GETBYID
        public Currency GetRecordById(int id)
        {
            var currency = RepositoryContext.Currencies.Where(x => x.Id == id).FirstOrDefault();
            return currency;
            
        }

        //Method DELETE
        public void RemoveRecord(int id, out bool check)
        {
            var currency = RepositoryContext.Currencies.Where(x => x.Id == id).FirstOrDefault();
            if(currency == null)
            {
                check = false;
            }
            else
            {
                check = true;
                RepositoryContext.Currencies.Remove(currency);
            }
            
        }

        public void SaveChanges()
        {
            RepositoryContext.SaveChanges();
        }

        //Method UPDATE
        public void UpdateRecord(int id, Currency newCurrency, out string ErrorMessage)
        {
            string errorMessage = string.Empty;

            var oldCurrency = RepositoryContext.Currencies.Where(x => x.Id == id).FirstOrDefault();
            if(oldCurrency == null)
            {
                ErrorMessage = "There is no Currency with this ID in Database";
            }else if (DuplicateValidation(newCurrency, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;
            }
            else
            {
                ErrorMessage = "Currency updated sucefully!";
                oldCurrency.Code = newCurrency.Code.ToUpper();
                oldCurrency.Description = newCurrency.Description;
                oldCurrency.ExchangeRate = newCurrency.ExchangeRate;
                oldCurrency.DateModified = DateTime.Now;
            }

        }

        //Code Duplicate Method
        public bool DuplicateValidation(Currency currency, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            var code = RepositoryContext.Currencies.Where(x => x.Code == currency.Code).FirstOrDefault();

            if (code != null)
            {
                ErrorMessage = "Code already exists in database! Record NOT added to database.";
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
