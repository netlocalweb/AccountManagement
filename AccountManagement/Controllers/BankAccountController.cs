using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/BankAccount/")]
    [ApiController]
    public class BankAccountController : Controller
    {
        
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IDapperRepository _dapperRepository;
     
        public BankAccountController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _dapperRepository = dapperRepository;
        }

        //POST: CREATE
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateBankAccountDTO createBankAccount)
        {
            var bankAccount = new BankAccount(createBankAccount.Code, createBankAccount.Name, createBankAccount.CurrencyId, createBankAccount.Balance, createBankAccount.ClientId, createBankAccount.IsActive);

            _repository.BankAccountRepository.CreateRecord(bankAccount, out string ErrorMessage);
            _repository.BankAccountRepository.SaveChanges();

            _logger.LogInfo(ErrorMessage);

            return Ok(ErrorMessage);

        }

        //GET: GETBYID
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var testStr = _repository.BankAccountRepository.GetRecordById(id);
            _logger.LogInfo("Get Bank Account record by id");
            return Ok(testStr);
        }

        //GET: GETALL
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var testStr = _repository.BankAccountRepository.GetAllRecords();

            _logger.LogInfo("Get all Bank Account records");

            return Ok(testStr);
        }

        //PUT: UPDATE
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] UpdateBankAccountDTO updateBankAccount)
        {
            var bankAccount = new BankAccount(updateBankAccount.Code, updateBankAccount.Name, updateBankAccount.CurrencyId, updateBankAccount.Balance, updateBankAccount.ClientId, updateBankAccount.IsActive);

            _repository.BankAccountRepository.UpdateRecord(id, bankAccount, out string ErrorMessage);
            _repository.BankAccountRepository.SaveChanges();
            _logger.LogInfo(ErrorMessage);

            return Ok(ErrorMessage);
        }

        //DELETE: DELETE
        [HttpDelete("inactive/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.BankAccountRepository.RemoveRecord(id);
            _repository.BankAccountRepository.SaveChanges();

            _logger.LogInfo("Bank Account Inactive");

            return Ok("Bank Account Inactive");
        }
    }
}
