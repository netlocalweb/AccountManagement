using AccountManagement.API.Models;
using AccountManagement.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccountManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountRepository repository;
        public BankAccountController(IBankAccountRepository repository)
        {
            this.repository = repository;

        }
        //Get client
        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetAll(int clientId)
        {
            var accounts = await repository.GetAllAsync(clientId);
            return Ok(accounts);
        }
        //Get account
        [HttpGet("account/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await repository.GetByIdAsync(id);
            if (account == null) return NotFound();
            return Ok(account);
        }
        //Create account
        [HttpPost]
        public async Task<IActionResult> Create(BankAccount account)
        {
            var created = await repository.AddAsync(account);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        //Update giving the id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BankAccount account)
        {
            if (id != account.Id) return BadRequest("ID is not matching");
            var updated = await repository.UpdateAsync(account);
            return Ok(updated);
        }
    }
}
