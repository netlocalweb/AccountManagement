using AccountManagement.API.Data;
using AccountManagement.API.Models;
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

        // Create
        public async Task<BankTransactionReadDto> AddTransactionAsync(BankTransactionCreateDto dto)
        {
            var account = await context.BankAccounts.FindAsync(dto.BankAccountId);
            if (account == null)
                throw new Exception("Bank account not found.");

            if (dto.Action == TransactionAction.Withdraw && account.Balance < dto.Amount)
                throw new Exception("Insufficient balance.");

            var transaction = new BankTransaction
            {
                BankAccountId = dto.BankAccountId,
                Action = dto.Action,
                Amount = dto.Amount,
                BankAccount = account
            };

            account.Balance += dto.Action == TransactionAction.Deposit ? dto.Amount : -dto.Amount;
            account.DateModified = DateTime.UtcNow;

            context.BankTransactions.Add(transaction);
            await context.SaveChangesAsync();

            return new BankTransactionReadDto
            {
                Id = transaction.Id,
                BankAccountId = transaction.BankAccountId,
                Action = transaction.Action,
                Amount = transaction.Amount,
                IsActive = transaction.IsActive,
                DateCreated = transaction.DateCreated
            };
        }

        // By Id
        public async Task<BankTransactionReadDto?> GetTransactionByIdAsync(int id)
        {
            var t = await context.BankTransactions.FindAsync(id);
            if (t == null) return null;

            return new BankTransactionReadDto
            {
                Id = t.Id,
                BankAccountId = t.BankAccountId,
                Action = t.Action,
                Amount = t.Amount,
                IsActive = t.IsActive,
                DateCreated = t.DateCreated
            };
        }

        //  All transactions by bank account
        public async Task<IEnumerable<BankTransactionReadDto>> GetTransactionsByAccountAsync(int bankAccountId)
        {
            return await context.BankTransactions
                .Where(t => t.BankAccountId == bankAccountId && t.IsActive)
                .Select(t => new BankTransactionReadDto
                {
                    Id = t.Id,
                    BankAccountId = t.BankAccountId,
                    Action = t.Action,
                    Amount = t.Amount,
                    IsActive = t.IsActive,
                    DateCreated = t.DateCreated
                })
                .ToListAsync();
        }
    }
}