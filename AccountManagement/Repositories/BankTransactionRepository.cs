using AccountManagement.Data;
using AccountManagement.Models;
using AccountManagement.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Repositories
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        private readonly AppDbContext context;

        public BankTransactionRepository(AppDbContext context)
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

            // Update balance based on action
            if (transaction.Action == TransactionAction.Deposit)
                account.Balance += transaction.Amount;
            else if (transaction.Action == TransactionAction.Withdraw)
            {
                if (account.Balance < transaction.Amount)
                    throw new InvalidOperationException("Balance is not enough.");
                account.Balance -= transaction.Amount;
            }

            context.BankTransactions.Add(transaction);
            account.DateModified = DateTime.UtcNow;

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