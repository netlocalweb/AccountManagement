using AccountManagement.API.Data;
using AccountManagement.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.API.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext context;

        public CurrencyRepository(AppDbContext context)
        {
            this.context= context;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await context.Currencies.ToListAsync();
        }

        public async Task<Currency> GetByIdAsync(int id)
        {
            return await context.Currencies.FindAsync(id);
        }
        public async Task<Currency> GetByCodeAsync(string code)
        {
            return await context.Currencies.FirstOrDefaultAsync(c => c.Code == code);
        }


        public async Task<Currency> AddAsync(Currency currency)
        {
            currency.Code = currency.Code.ToUpper();
            currency.DateCreated = System.DateTime.UtcNow;

            context.Currencies.Add(currency);
            await context.SaveChangesAsync();
            return currency;
        }

        public async Task<Currency> UpdateAsync(Currency currency)
        {
            currency.DateModified = System.DateTime.UtcNow;
            context.Currencies.Update(currency);
            await context.SaveChangesAsync();
            return currency;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var currency = await context.Currencies.FindAsync(id);
            if (currency == null)
                return false;

            context.Currencies.Remove(currency);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

