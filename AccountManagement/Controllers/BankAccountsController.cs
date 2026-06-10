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
    [Route("api/bank-accounts")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public BankAccountsController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankAccount([FromBody] BankAccountForCreationDto bankAccountDto)
        {
            if (bankAccountDto == null)
                return BadRequest("Bank account data is null.");

            if (string.IsNullOrWhiteSpace(bankAccountDto.Code))
                return BadRequest("Code is required.");

            if (string.IsNullOrWhiteSpace(bankAccountDto.Name))
                return BadRequest("Name is required.");

            var exists = await _repositoryContext.BankAccounts
                .AnyAsync(a => a.ClientId == bankAccountDto.ClientId && a.Code == bankAccountDto.Code);

            if (exists)
                return Conflict("Bank account code already exists for this client.");

            var bankAccount = new BankAccount
            {
                Code = bankAccountDto.Code,
                Name = bankAccountDto.Name,
                CurrencyId = bankAccountDto.CurrencyId,
                Balance = bankAccountDto.Balance,
                ClientId = bankAccountDto.ClientId,
                IsActive = bankAccountDto.IsActive,
                DateCreated = DateTime.UtcNow
            };

            _repositoryContext.BankAccounts.Add(bankAccount);
            await _repositoryContext.SaveChangesAsync();

            return Ok(new { bankAccount.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetBankAccounts([FromQuery] int? clientId = null)
        {
            var query = _repositoryContext.BankAccounts.AsNoTracking();

            if (clientId.HasValue)
                query = query.Where(a => a.ClientId == clientId.Value);

            var accounts = await query
                .OrderBy(a => a.Code)
                .Select(a => new BankAccountDto
                {
                    Id = a.Id,
                    Code = a.Code,
                    Name = a.Name,
                    CurrencyId = a.CurrencyId,
                    Balance = a.Balance,
                    ClientId = a.ClientId,
                    IsActive = a.IsActive,
                    DateCreated = a.DateCreated,
                    DateModified = a.DateModified
                })
                .ToListAsync();

            return Ok(accounts);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBankAccount(int id, [FromBody] BankAccountForUpdateDto bankAccountDto)
        {
            if (bankAccountDto == null)
                return BadRequest("Bank account data is null.");

            if (string.IsNullOrWhiteSpace(bankAccountDto.Code))
                return BadRequest("Code is required.");

            if (string.IsNullOrWhiteSpace(bankAccountDto.Name))
                return BadRequest("Name is required.");

            var bankAccount = await _repositoryContext.BankAccounts.FindAsync(id);
            if (bankAccount == null)
                return NotFound($"Bank account with id {id} not found.");

            var exists = await _repositoryContext.BankAccounts
                .AnyAsync(a => a.Id != id && a.ClientId == bankAccountDto.ClientId && a.Code == bankAccountDto.Code);

            if (exists)
                return Conflict("Bank account code already exists for this client.");

            bankAccount.Code = bankAccountDto.Code;
            bankAccount.Name = bankAccountDto.Name;
            bankAccount.CurrencyId = bankAccountDto.CurrencyId;
            bankAccount.Balance = bankAccountDto.Balance;
            bankAccount.ClientId = bankAccountDto.ClientId;
            bankAccount.IsActive = bankAccountDto.IsActive;
            bankAccount.DateModified = DateTime.UtcNow;

            await _repositoryContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBankAccount(int id)
        {
            var bankAccount = await _repositoryContext.BankAccounts.FindAsync(id);
            if (bankAccount == null)
                return NotFound($"Bank account with id {id} not found.");

            bankAccount.IsActive = false;
            bankAccount.DateModified = DateTime.UtcNow;
            await _repositoryContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
