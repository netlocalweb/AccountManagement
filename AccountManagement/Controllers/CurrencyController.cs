using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/currency/")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IDapperRepository _dapperRepository;

        public CurrencyController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _dapperRepository = dapperRepository;
        }

        //POST: CREATE
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateCurrencyDTO createCurrencyDTO)
        {
            var currency = new Currency(createCurrencyDTO.Code, createCurrencyDTO.Description, createCurrencyDTO.ExchangeRate);

            _repository.CurrencyRepository.CreateRecord(currency, out string ErrorMessage);
            _repository.CurrencyRepository.SaveChanges();

            _logger.LogInfo(ErrorMessage);

            return Ok(ErrorMessage);

        }

        //GET: GETBYID
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var testStr = _repository.CurrencyRepository.GetRecordById(id);
            _logger.LogInfo("Get currencies records by id");
            return Ok(testStr);
        }

        //GET: GETALL
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var testStr = _repository.CurrencyRepository.GetAllRecords();

            _logger.LogInfo("Get all Currencies records");

            return Ok(testStr);
        }

        //PUT: UPDATE
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] CreateCurrencyDTO createCurrencyDTO)
        {
            var currencyUpdated = new Currency(createCurrencyDTO.Code, createCurrencyDTO.Description, createCurrencyDTO.ExchangeRate);

            _repository.CurrencyRepository.UpdateRecord(id, currencyUpdated, out string ErrorMessage);
            _repository.CurrencyRepository.SaveChanges();
            _logger.LogInfo(ErrorMessage);

            return Ok(ErrorMessage);
        }

        //DELETE: DELETE
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.CurrencyRepository.RemoveRecord(id);
            _repository.CurrencyRepository.SaveChanges();

            _logger.LogInfo("Delete a currency record");

            return Ok("Currency deleted form database.");
        }
    }
}
