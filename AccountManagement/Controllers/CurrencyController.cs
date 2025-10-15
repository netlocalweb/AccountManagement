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
            currencyDto.Code = currencyDto.Code.ToUpper().Trim();//upercase

            if (currencyDto is null)
                return BadRequest("Currency code must be unique!");
            
            var currency = _mapper.Map<Currency>(currencyDto);
            _repositoryManager.Currency.CreateCurrency(currency);
            await _repositoryManager.SaveAsync();
            return Ok(currency);
        }

    }
}
