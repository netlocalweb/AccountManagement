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
                .Where(b => b.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<BankAccount> GetByIdAsync(int id)
        {
            return await context.BankAccounts.FindAsync(id);
        }

        public async Task<BankAccount> AddAsync(BankAccount account)
        {
            account.DateCreated = DateTime.UtcNow;
            account.IsActive = true;

            context.BankAccounts.Add(account);
            await context.SaveChangesAsync();
            return account;
        }

        public async Task<BankAccount> UpdateAsync(BankAccount account)
        {
            account.DateModified = DateTime.UtcNow;
            context.BankAccounts.Update(account);
            await context.SaveChangesAsync();
            return account;
        }
    }

   }

