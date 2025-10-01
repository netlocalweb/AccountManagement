using AccountManagement.API.Models;
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
        public async Task<IActionResult> Create(BankTransaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await repository.AddAsync(transaction);
            return Ok(created);
        }
    }
}
