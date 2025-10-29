using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly IBankTransactionRepository transactionRepository;
        private readonly IBankAccountRepository accountRepository;
        private readonly IMapper mapper;

        public BankTransactionController(IBankTransactionRepository repository,
            IBankTransactionRepository transactionRepository,
            IBankAccountRepository accountRepository,
            IMapper mapper)
        {
                this.transactionRepository = transactionRepository;
                this.accountRepository = accountRepository;
                this.mapper = mapper;
            
        }

        // Get all transactions which are active
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int bankAccountId)
        {
            if (bankAccountId <= 0)
                return BadRequest("BankAccountId must be greater than zero.");

            var account = await accountRepository.GetByIdAsync(bankAccountId);
            if (account == null)
                return NotFound($"Bank account with ID {bankAccountId} not found.");

            var transactions = await transactionRepository.GetAllAsync(bankAccountId);
            var dtos = mapper.Map<IEnumerable<BankTransactionReadDto>>(transactions);
            return Ok(dtos);
        }

        // Get by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await transactionRepository.GetByIdAsync(id); 
            if (transaction == null) 
                return NotFound();

            var dto = mapper.Map<BankTransactionReadDto>(transaction);
            return Ok(dto);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(BankTransactionCreateDto dto)
        {
            var account = await accountRepository.GetByIdAsync(dto.BankAccountId);
            if (account == null)
                return NotFound("Bank account was not found.");

            
            if (dto.Action == TransactionAction.Withdraw && account.Balance < dto.Amount)
                return BadRequest("There is not enought balance.");

            if (dto.Action == TransactionAction.Deposit)
                account.Balance += dto.Amount;
            else if (dto.Action == TransactionAction.Withdraw)
                account.Balance -= dto.Amount;

            account.DateModified = DateTime.UtcNow;

            // Map transaction
            var transaction = mapper.Map<BankTransaction>(dto);
            transaction.IsActive = true;
            transaction.DateCreated = DateTime.UtcNow;

            await transactionRepository.AddAsync(transaction);
            await accountRepository.UpdateAsync(account);

            var readDto = mapper.Map<BankTransactionReadDto>(transaction);
            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }


        // Soft delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await transactionRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound("Transaction not found.");

            var deleted = await transactionRepository.SoftDeleteAsync(id);
            if (!deleted)
                return BadRequest("Could not delete transaction.");

            return NoContent();
        }
    }
}