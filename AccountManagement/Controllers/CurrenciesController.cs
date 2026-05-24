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
    [Route("api/currencies")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public CurrenciesController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrency([FromBody] CurrencyForCreationDto currencyDto)
        {
            if (currencyDto == null)
                return BadRequest("Currency data is null.");

            if (string.IsNullOrWhiteSpace(currencyDto.Code))
                return BadRequest("Code is required.");

            var code = currencyDto.Code.Trim().ToUpperInvariant();

            var exists = await _repositoryContext.Currencies
                .AnyAsync(c => c.Code == code);

            if (exists)
                return Conflict("Currency code already exists.");

            var currency = new Currency
            {
                Code = code,
                Description = currencyDto.Description,
                ExchangeRate = currencyDto.ExchangeRate,
                DateCreated = DateTime.UtcNow
            };

            _repositoryContext.Currencies.Add(currency);
            await _repositoryContext.SaveChangesAsync();

            return Ok(new { currency.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrencies()
        {
            var currencies = await _repositoryContext.Currencies
                .AsNoTracking()
                .OrderBy(c => c.Code)
                .Select(c => new CurrencyDto
                {
                    Id = c.Id,
                    Code = c.Code,
                    Description = c.Description,
                    ExchangeRate = c.ExchangeRate,
                    DateCreated = c.DateCreated,
                    DateModified = c.DateModified
                })
                .ToListAsync();

            return Ok(currencies);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCurrency(int id, [FromBody] CurrencyForUpdateDto currencyDto)
        {
            if (currencyDto == null)
                return BadRequest("Currency data is null.");

            if (string.IsNullOrWhiteSpace(currencyDto.Code))
                return BadRequest("Code is required.");

            var currency = await _repositoryContext.Currencies.FindAsync(id);
            if (currency == null)
                return NotFound($"Currency with id {id} not found.");

            var code = currencyDto.Code.Trim().ToUpperInvariant();

            var exists = await _repositoryContext.Currencies
                .AnyAsync(c => c.Id != id && c.Code == code);

            if (exists)
                return Conflict("Currency code already exists.");

            currency.Code = code;
            currency.Description = currencyDto.Description;
            currency.ExchangeRate = currencyDto.ExchangeRate;
            currency.DateModified = DateTime.UtcNow;

            await _repositoryContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var currency = await _repositoryContext.Currencies.FindAsync(id);
            if (currency == null)
                return NotFound($"Currency with id {id} not found.");

            _repositoryContext.Currencies.Remove(currency);
            await _repositoryContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
