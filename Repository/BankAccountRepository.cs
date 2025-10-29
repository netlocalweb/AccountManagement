using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;  
using System.Linq;
using Contracts;


namespace Repository
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly RepositoryContext context;

        public BankAccountRepository(RepositoryContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<BankAccount>> GetAllAsync(int clientId)
        {
            return await context.BankAccounts
                .Where(b => b.ClientId == clientId && b.IsActive)
                .ToListAsync();
        }


        public async Task<BankAccount> GetByIdAsync(int id)
        {
            return await context.BankAccounts.FindAsync(id);
        }

        public async Task<bool> ExistsByCodeAsync(int clientId, string code, int? excludeId = null)
        {
            var query = context.BankAccounts
                .Where(a => a.ClientId == clientId && a.Code.ToUpper() == code.ToUpper());

            if (excludeId.HasValue)
                query = query.Where(a => a.Id != excludeId.Value);

            return await query.AnyAsync();
        }


        public async Task<BankAccount> AddAsync(BankAccount account)
        {
            
            context.BankAccounts.Add(account);
            await context.SaveChangesAsync();
            return account;
        }

        public async Task<BankAccount> UpdateAsync(BankAccount account)
        {
            var existing = await context.BankAccounts.FindAsync(account.Id);
            if (existing == null) return null;

            existing.Code = account.Code;
            existing.Name = account.Name;
            existing.CurrencyId = account.CurrencyId;
            existing.IsActive = account.IsActive;
            existing.DateModified = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var existing = await context.BankAccounts.FindAsync(id);
            if (existing == null) 
                return false;

            existing.IsActive = false;
            existing.DateModified = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }


    }

}

