using AccountManagement.Models;
using AccountManagement.Repositories;
using AccountManagement.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Authorize]
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
            var result = new List<CurrencyReadDto>();

            foreach (var c in currencies)
            {
                result.Add(new CurrencyReadDto
                {
                    Id = c.Id,
                    Code = c.Code,
                    Description = c.Description,
                    ExchangeRate = c.ExchangeRate
                });
            }

            return Ok(result);
        }

        //Get currency by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var currency = await repository.GetByIdAsync(id);
            if (currency == null) return NotFound();

            var dto = new CurrencyReadDto
            {
                Id = currency.Id,
                Code = currency.Code,
                Description = currency.Description,
                ExchangeRate = currency.ExchangeRate
            };

            return Ok(dto);
        }

        //Create new currency
        [HttpPost]
        public async Task<IActionResult> Create(CurrencyCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Code) || string.IsNullOrWhiteSpace(dto.Description))
                return BadRequest("Code and Description are required.");

            if (await repository.GetByCodeAsync(dto.Code.ToUpper()) != null)
                return Conflict("Currency code already exists.");

            var created = await repository.AddAsync(new Currency
            {
                Code = dto.Code.ToUpper(),
                Description = dto.Description,
                ExchangeRate = dto.ExchangeRate
            });

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new CurrencyReadDto
            {
                Id = created.Id,
                Code = created.Code,
                Description = created.Description,
                ExchangeRate = created.ExchangeRate
            });
        }

        // PUT: api/currency/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CurrencyUpdateDto dto)
        {
            var currency = await repository.GetByIdAsync(id);
            if (currency == null) return NotFound();

            currency.Description = dto.Description;
            currency.ExchangeRate = dto.ExchangeRate;

            var updated = await repository.UpdateAsync(currency);

            return Ok(new CurrencyReadDto
            {
                Id = updated.Id,
                Code = updated.Code,
                Description = updated.Description,
                ExchangeRate = updated.ExchangeRate
            });
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

