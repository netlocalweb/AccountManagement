using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public CurrencyRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Create(Currency currency)
        {
            _repositoryContext.Currencies.Add(currency);
            _repositoryContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var currency = _repositoryContext.Currencies.Find(id);
            if (currency != null)
            {
                _repositoryContext.Currencies.Remove(currency);
                _repositoryContext.SaveChanges();
            }
        }

        public ICollection<Currency> FindAll()
        {
            return _repositoryContext.Currencies.ToList();
        }

        public Currency FindById(int id)
        {
            return _repositoryContext.Currencies.Find(id);
        }

        public bool Update(Currency entity)
        {
            var existingCurrency = _repositoryContext.Currencies.Find(entity.Id);

            //whatever to-change properties do i shof me von
            existingCurrency.DateModified = DateTime.Now;

            // Save the changes
            _repositoryContext.SaveChanges();
            return true;
        }

    }
}
