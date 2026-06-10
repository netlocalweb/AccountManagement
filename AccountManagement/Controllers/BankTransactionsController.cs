using Entities;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/bank-transactions")]
    [ApiController]
    public class BankTransactionsController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public BankTransactionsController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankTransaction([FromBody] BankTransactionForCreationDto dto)
        {
            if (dto == null)
                return BadRequest("Transaction data is null.");

            var account = await _repositoryContext.BankAccounts.FindAsync(dto.BankAccountId);
            if (account == null)
                return NotFound($"Bank account {dto.BankAccountId} not found.");

            if (!account.IsActive)
                return BadRequest("Cannot perform a transaction on an inactive account.");

            if (dto.Action == 2 && account.Balance < dto.Amount) // Assuming 1 = Depozitim, 2 = Terheqje
                return BadRequest("Insufficient funds for withdrawal.");

            if (dto.Action == 1)
            {
                account.Balance += dto.Amount;
            }
            else if (dto.Action == 2)
            {
                account.Balance -= dto.Amount;
            }
            else
            {
                return BadRequest("Invalid action type. Must be 1 (Depozitim) or 2 (Terheqje).");
            }

            account.DateModified = DateTime.UtcNow;

            var transaction = new BankTransaction
            {
                BankAccountId = dto.BankAccountId,
                Action = dto.Action,
                Amount = dto.Amount,
                IsActive = true,
                DateCreated = DateTime.UtcNow
            };

            _repositoryContext.BankTransactions.Add(transaction);
            await _repositoryContext.SaveChangesAsync();

            return Ok(new { transaction.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetBankTransactions([FromQuery] int? bankAccountId = null)
        {
            var query = _repositoryContext.BankTransactions.AsNoTracking().Where(t => t.IsActive);

            if (bankAccountId.HasValue)
                query = query.Where(t => t.BankAccountId == bankAccountId.Value);

            var transactions = await query
                .OrderByDescending(t => t.DateCreated)
                .Select(t => new BankTransactionDto
                {
                    Id = t.Id,
                    BankAccountId = t.BankAccountId,
                    Action = t.Action,
                    Amount = t.Amount,
                    IsActive = t.IsActive,
                    DateCreated = t.DateCreated,
                    DateModified = t.DateModified
                })
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBankTransaction(int id, [FromBody] BankTransactionForUpdateDto dto)
        {
            if (dto == null)
                return BadRequest("Transaction data is null.");

            var transaction = await _repositoryContext.BankTransactions.FindAsync(id);
            if (transaction == null)
                return NotFound($"Bank transaction with id {id} not found.");

            transaction.BankAccountId = dto.BankAccountId;
            transaction.Action = dto.Action;
            transaction.Amount = dto.Amount;
            transaction.IsActive = dto.IsActive;
            transaction.DateModified = DateTime.UtcNow;

            await _repositoryContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBankTransaction(int id)
        {
            var transaction = await _repositoryContext.BankTransactions.FindAsync(id);
            if (transaction == null)
                return NotFound($"Bank transaction with id {id} not found.");

            transaction.IsActive = false;
            transaction.DateModified = DateTime.UtcNow;

            await _repositoryContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
