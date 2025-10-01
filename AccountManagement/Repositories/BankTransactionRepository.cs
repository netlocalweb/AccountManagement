using AccountManagement.API.Data;
using AccountManagement.API.Models;
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
        public async Task<IEnumerable<BankTransaction>> GetAllAsync()
        {
            return await context.BankTransactions.ToListAsync();
        }


        public async Task<BankTransaction> GetByIdAsync(int id)
        {
            return await context.BankTransactions.FindAsync(id);
        }
        public async Task<BankTransaction> AddAsync(BankTransaction transaction)
        {
            var account = await context.BankAccounts.FindAsync(transaction.BankAccountId);
            if (account == null)
                throw new Exception("Bank account not found.");

            // Update balance based on action
            if (transaction.Action == TransactionAction.Deposit)
            {
                account.Balance += transaction.Amount;
            }
            else if (transaction.Action == TransactionAction.Withdraw)
            {
                if (account.Balance < transaction.Amount)
                    throw new Exception("Insufficient is not enough.");

                account.Balance -= transaction.Amount;
            }

            transaction.DateCreated = DateTime.UtcNow;

            context.BankTransactions.Add(transaction);
            await context.SaveChangesAsync();

            return transaction;
          
        }
    }
}
