using AccountManagement.API.Data;
using AccountManagement.API.Models;
using AccountManagement.API.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;  
using System.Linq;                  


namespace AccountManagement.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly AppDbContext context;

        public BankAccountRepository(AppDbContext context)
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
            if (account.Balance >= 0) existing.Balance = account.Balance;
            existing.DateModified = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var existing = await context.BankAccounts.FindAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            existing.DateModified = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }

    }

}

