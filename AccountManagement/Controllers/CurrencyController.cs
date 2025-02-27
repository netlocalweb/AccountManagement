using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        public CurrencyController(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCurrencies()
        {
            var currencies = _currencyRepository.FindAll();
            var currencyDTOs = _mapper.Map<List<CurrencyDTO>>(currencies);
            return Ok(currencyDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetCurrencyById(int id)
        {
            var currency = _currencyRepository.FindById(id);
            if (currency == null)
            {
                return NotFound();
            }
            var currencyDTO = _mapper.Map<CurrencyDTO>(currency);
            return Ok(currencyDTO);
        }

        [HttpPost]
        public IActionResult AddCurrency(AddCurrencyDTO addCurrencyDTO)
        {
            if (addCurrencyDTO == null)
            {
                return BadRequest();
            }

            var currency = _mapper.Map<Currency>(addCurrencyDTO);
            _currencyRepository.Create(currency);

            var currencyDTO = _mapper.Map<CurrencyDTO>(currency);
            return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currencyDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCurrency(int id, AddCurrencyDTO addCurrencyDTO)
        {
            if (addCurrencyDTO == null || id != addCurrencyDTO.Id)
            {
                return BadRequest();
            }

            var existingCurrency = _currencyRepository.FindById(id);
            if (existingCurrency == null)
            {
                return NotFound();
            }

            var currency = _mapper.Map<Currency>(addCurrencyDTO);
            _currencyRepository.Update(currency);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCurrency(int id)
        {
            var currency = _currencyRepository.FindById(id);
            if (currency == null)
            {
                return NotFound();
            }

            _currencyRepository.Delete(id);
            return NoContent();
        }
    }
}
