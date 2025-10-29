using Entities;
using Entities.Models;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;

namespace Repository
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        private readonly RepositoryContext context;

        public BankTransactionRepository(RepositoryContext context)
        {
            this.context = context;
        }
        // Get all active transactions
        public async Task<IEnumerable<BankTransaction>> GetAllAsync(int bankAccountId)
        {
            return await context.BankTransactions
                .Where(t => t.BankAccountId == bankAccountId && t.IsActive)
                .ToListAsync();
        }

        //Get by id
        public async Task<BankTransaction?> GetByIdAsync(int id)
        {
            return await context.BankTransactions.FindAsync(id);
        }


        public async Task<BankTransaction> AddAsync(BankTransaction transaction)
        {
            var account = await context.BankAccounts.FindAsync(transaction.BankAccountId);
            if (account == null) return null;

            context.BankTransactions.Add(transaction);
            await context.SaveChangesAsync();
            return transaction;
        }

        //Soft Delete
        public async Task<bool> SoftDeleteAsync(int id)
        {
            var transaction = await context.BankTransactions.FindAsync(id);
            if (transaction == null) return false;

            transaction.IsActive = false;
            transaction.DateModified = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }
    }
}