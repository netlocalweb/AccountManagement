using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AccountManagement.Controllers
{
    [Route("api/currency/[action]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public CurrencyController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var currencies = await _repository.CurrencyRepository.GetAllCurrenciesAsync();

            var currenciesDto = currencies.Select(currency => new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                Description = currency.Description,
                ExchangeRate = currency.ExchangeRate,
                DateCreated = currency.DateCreated,
                DateModified = currency.DateModified
            });

            return Ok(currenciesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyById(int id)
        {
            var currency = await _repository.CurrencyRepository.GetCurrencyByIdAsync(id);

            if (currency == null)
                return NotFound("Currency not found.");

            var currencyDto = new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                Description = currency.Description,
                ExchangeRate = currency.ExchangeRate,
                DateCreated = currency.DateCreated,
                DateModified = currency.DateModified
            };

            return Ok(currencyDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrency([FromBody] CreateCurrencyDTO currencyDto)
        {
            if (currencyDto == null)
                return BadRequest("Currency data is null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (currencyDto.ExchangeRate <= 0)
                return BadRequest("ExchangeRate must be greater than 0.");

            var code = currencyDto.Code.Trim().ToUpper();

            var codeExists = await _repository.CurrencyRepository.GetCurrencyByCodeAsync(code);
            if (codeExists != null)
                return BadRequest("Currency code already exists.");

            var currency = new Currency
            {
                Code = code,
                Description = currencyDto.Description.Trim(),
                ExchangeRate = currencyDto.ExchangeRate,
                DateCreated = DateTime.Now
            };

            _repository.CurrencyRepository.CreateCurrency(currency);
            await _repository.SaveAsync();

            _logger.LogInfo("Currency created successfully.");

            var createdCurrencyDto = new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                Description = currency.Description,
                ExchangeRate = currency.ExchangeRate,
                DateCreated = currency.DateCreated,
                DateModified = currency.DateModified
            };

            return Ok(createdCurrencyDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrency(int id, [FromBody] UpdateCurrencyDTO currencyDto)
        {
            if (currencyDto == null)
                return BadRequest("Currency data is null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (currencyDto.ExchangeRate <= 0)
                return BadRequest("ExchangeRate must be greater than 0.");

            var currency = await _repository.CurrencyRepository.GetCurrencyByIdAsync(id);

            if (currency == null)
                return NotFound("Currency not found.");

            var code = currencyDto.Code.Trim().ToUpper();

            var codeExists = await _repository.CurrencyRepository.GetCurrencyByCodeAsync(code);
            if (codeExists != null && codeExists.Id != id)
                return BadRequest("Currency code already exists.");

            currency.Code = code;
            currency.Description = currencyDto.Description.Trim();
            currency.ExchangeRate = currencyDto.ExchangeRate;
            currency.DateModified = DateTime.Now;

            _repository.CurrencyRepository.UpdateCurrency(currency);
            await _repository.SaveAsync();

            _logger.LogInfo("Currency updated successfully.");

            var updatedCurrencyDto = new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                Description = currency.Description,
                ExchangeRate = currency.ExchangeRate,
                DateCreated = currency.DateCreated,
                DateModified = currency.DateModified
            };

            return Ok(updatedCurrencyDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var currency = await _repository.CurrencyRepository.GetCurrencyByIdAsync(id);

            if (currency == null)
                return NotFound("Currency not found.");

            _repository.CurrencyRepository.DeleteCurrency(currency);
            await _repository.SaveAsync();

            _logger.LogInfo("Currency deleted successfully.");

            return Ok("Currency deleted successfully.");
        }
    }
}