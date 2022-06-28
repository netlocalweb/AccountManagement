using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AccountManagement.Controllers
{
    [Route("api/BankTransaction/")]
    [ApiController]
    public class BankTransactionController : Controller
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IDapperRepository _dapperRepository;

        public BankTransactionController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _dapperRepository = dapperRepository;
        }

        //POST: CREATE
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateBankTransactionDTO transaction)
        {
            var newTransaction = new BankTransaction(transaction.BankAccountId, transaction.Action, transaction.Amount);
            _repository.BankTransactionRepository.CreateRecord(newTransaction, out string ErrorMessage);
            _repository.BankTransactionRepository.SaveChanges();

            _logger.LogInfo(ErrorMessage);

            return Ok(ErrorMessage);

        }

        //GET: GETBYID
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var testStr = _repository.BankTransactionRepository.GetRecordById(id);
            _logger.LogInfo("Get Bank Account record by id");
            if (testStr == null)
            {
                return NotFound("There is no Bank Transaction with this ID in Database");
            }
            else
            {
                return Ok(testStr);
            }
        }

        //GET: GETALL
        [HttpPost("getall")]
        public IActionResult GetAll([FromBody] PagingParameter pagingParameter)
        {
            var testStr = _repository.BankTransactionRepository.GetAllRecords(pagingParameter.PageNumber, pagingParameter.PageSize, out int totalRecords);
            var pageInfo = new Pager<IEnumerable<BankTransaction>>(totalRecords, pagingParameter.PageNumber, pagingParameter.PageSize, data: testStr);
            if (pagingParameter.PageNumber > pageInfo.TotalPages)
            {
                return BadRequest("The records you are requesting have less pages than your requested page number!");
            }
            else
            {
                _logger.LogInfo("Get all Category records");

                return Ok(pageInfo);
            }
        }



        //DELETE: DELETE
        [HttpDelete("inactive/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.BankTransactionRepository.RemoveRecord(id, out bool check);
            if(check == false)
            {
                return NotFound("There is no Bank Transaction with this ID in Database");
            }
            else
            {
                _repository.BankTransactionRepository.SaveChanges();

                _logger.LogInfo("Make transaction inactive");

                return Ok("Transaction Inactive");
            }
            
        }
    }
}
