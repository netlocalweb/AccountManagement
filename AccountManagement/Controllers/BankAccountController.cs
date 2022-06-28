using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
        //[Authorize]
        public IActionResult Create([FromBody] CreateBankAccountDTO createBankAccount)
        {
            var getTokenId = "";
            try
            {
                getTokenId = HttpContext.User.Claims.First(x => x.Type == "Id").Value;
                var getClientId = Int32.Parse(getTokenId);
                var bankAccount = new BankAccount(createBankAccount.Code, createBankAccount.Name, createBankAccount.CurrencyId, createBankAccount.Balance, getClientId);

                _repository.BankAccountRepository.CreateRecord(bankAccount, out string ErrorMessage);
                _repository.BankAccountRepository.SaveChanges();

                _logger.LogInfo(ErrorMessage);

                return Ok(ErrorMessage);
            }
            catch (Exception)
            {
                return BadRequest("You need to log in first in order to create a Bank Account");
            }
        }

        //GET: GETBYID
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var testStr = _repository.BankAccountRepository.GetRecordById(id,  out int validation);
            _logger.LogInfo("Get Bank Account record by id");
            if(validation == 0)
            {
                return NotFound("Bank Account not found");
            }
            else if(validation == 1)
            {
                return Ok("Bank Account is inactive");
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
            var testStr = _repository.BankAccountRepository.GetAllRecords(pagingParameter.PageNumber, pagingParameter.PageSize, out int totalRecords);
            var pageInfo = new Pager<IEnumerable<BankAccount>>(totalRecords, pagingParameter.PageNumber, pagingParameter.PageSize, data: testStr);
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

        //PUT: UPDATE
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] UpdateBankAccountDTO updateBankAccount)
        {
            var bankAccount = new BankAccount(updateBankAccount.Code, updateBankAccount.Name, updateBankAccount.CurrencyId, updateBankAccount.Balance, updateBankAccount.ClientId);

            _repository.BankAccountRepository.UpdateRecord(id, bankAccount, out string ErrorMessage);
            _repository.BankAccountRepository.SaveChanges();
            _logger.LogInfo(ErrorMessage);

            return Ok(ErrorMessage);
        }

        //DELETE: DELETE
        [HttpDelete("inactive/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.BankAccountRepository.RemoveRecord(id, out bool check);
            if(check == false)
            {
                return NotFound("There is no Bank Account with this ID in Database");
            }
            else
            {
                _repository.BankAccountRepository.SaveChanges();

                _logger.LogInfo("Bank Account Inactive");

                return Ok("Bank Account Inactive");
            }
            
        }
    }
}
