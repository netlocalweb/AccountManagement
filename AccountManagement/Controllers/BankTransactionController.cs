using AccountManagement.Models;
using AccountManagement.Models.DTOs;
using AccountManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly IBankTransactionRepository repository;

        public BankTransactionController(IBankTransactionRepository repository)
        {
            this.repository = repository;
        }

        // Get all transactions which are active
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int bankAccountId)
        {
            if (bankAccountId <= 0)
                return BadRequest("BankAccountId must be provided and greater than zero.");

            var transactions = await repository.GetAllAsync(bankAccountId);
            var dtos = transactions.Select(t => new BankTransactionReadDto
            {
                Id = t.Id,
                BankAccountId = t.BankAccountId,
                Action = t.Action,
                Amount = t.Amount,
                IsActive = t.IsActive,
                DateCreated = t.DateCreated,
                DateModified = t.DateModified
            }).ToList();

            return Ok(dtos);
        }

        // Get by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await repository.GetByIdAsync(id);
            if (transaction == null) return NotFound();

            var dto = new BankTransactionReadDto
            {
                Id = transaction.Id,
                BankAccountId = transaction.BankAccountId,
                Action = transaction.Action,
                Amount = transaction.Amount,
                IsActive = transaction.IsActive,
                DateCreated = transaction.DateCreated,
                DateModified = transaction.DateModified
            };

            return Ok(dto);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(BankTransactionCreateDto dto)
        {
            var transaction = new BankTransaction
            {
                BankAccountId = dto.BankAccountId,
                Action = dto.Action,
                Amount = dto.Amount,
                IsActive = true,
                DateCreated = System.DateTime.UtcNow
            };

            try
            {
                var created = await repository.AddAsync(transaction);
                if (created == null) return NotFound("Bank account was not found.");

                var readDto = new BankTransactionReadDto
                {
                    Id = created.Id,
                    BankAccountId = created.BankAccountId,
                    Action = created.Action,
                    Amount = created.Amount,
                    IsActive = created.IsActive,
                    DateCreated = created.DateCreated
                };

                return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        // Soft delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await repository.SoftDeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent(); 
        }
    }
}