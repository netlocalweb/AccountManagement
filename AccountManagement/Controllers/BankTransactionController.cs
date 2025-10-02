using AccountManagement.API.Models;
using AccountManagement.Models.DTOs;
using AccountManagement.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly IBankTransactionRepository repository;

        public BankTransactionController(IBankTransactionRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BankTransactionCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var transaction = await repository.AddTransactionAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await repository.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();
            return Ok(transaction);
        }

        [HttpGet("account/{bankAccountId}")]
        public async Task<IActionResult> GetByAccount(int bankAccountId)
        {
            var transactions = await repository.GetTransactionsByAccountAsync(bankAccountId);
            return Ok(transactions);
        }
    }
}