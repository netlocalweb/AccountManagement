using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController( IRepositoryManager repositoryManager , IMapper mapper , ILogger<CurrencyController> logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        //Get all currencies
        //GET : api/currency
        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var currencies = await _repositoryManager.Currency.GetAllCurrenciesAsync(trackChanges:false);
            var currencyDto = _mapper.Map<IEnumerable<CurrencyDto>>(currencies);
            return Ok(currencyDto);
        }

        //Get currency by id
        //GET : api/currency/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCurrencyById(int id)
        {
            var currency = await _repositoryManager.Currency.GetCurrencyByIdAsync(id, trackChanges: false);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CurrencyDto>(currency));
        }

        //Create currency
        //POST: api/currency
        [HttpPost]
        public async Task<IActionResult> CreateCurrency([FromBody] CurrencyCreationDto currencyDto)
        {
            if (currencyDto == null)
                return BadRequest("Currency is null");

            currencyDto.Code = currencyDto.Code.ToUpper();

            var currency = _mapper.Map<Currency>(currencyDto);
            currency.DateCreated = DateTime.UtcNow;

            _repositoryManager.Currency.CreateCurrency(currency);
            await _repositoryManager.SaveAsync();

            var currencyToReturn = _mapper.Map<CurrencyDto>(currency);
            return CreatedAtAction(nameof(GetCurrencyById), new { id = currencyToReturn.Id }, currencyToReturn);

        }

        //Delete currency 
        //DELETE:api/currency
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult>DeleteCurrency(int id)
        {
            var currency = await _repositoryManager.Currency.GetCurrencyByIdAsync(id , trackChanges: false);
            if ( currency == null)
                return NotFound();

            _repositoryManager.Currency.DeleteCurrency(currency);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

    }
}
