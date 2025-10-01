using AccountManagement.API.Models;
using AccountManagement.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccountManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository repository;

        public CurrencyController(ICurrencyRepository repository)
        {
            this.repository = repository;
        }
        //Get all currencies
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currencies = await repository.GetAllAsync();
            return Ok(currencies);
        }

        //Get currency by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var currency = await repository.GetByIdAsync(id);
            if (currency == null) return NotFound();//if not found
            return Ok(currency);//if found
        }

        //Create new currency
        [HttpPost]
        public async Task<IActionResult> Create(Currency currency)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await repository.AddAsync(currency);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        //Update existing currency
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Currency currency)
        {
            if (id != currency.Id) return BadRequest("ID is not matching");
            var updated = await repository.UpdateAsync(currency);
            return Ok(updated);
        }

        //Delete currency
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var deleted = await repository.DeleteAsync(id);
            if (!deleted) return NotFound();//If not found
            return NoContent();//if found
        }
    };
}
