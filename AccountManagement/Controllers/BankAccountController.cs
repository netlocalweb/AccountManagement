using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<BankAccountController> _logger;
        public BankAccountController(IRepositoryManager repositoryManager, IMapper mapper, ILogger<BankAccountController> logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        //Get all bank accounts 
        //GET:api/bankaccounts
        [HttpGet]
        public async Task<IActionResult> GetAllBankAccounts()
        {
            var account = await _repositoryManager.BankAccount.GetAllBAnkAccountsAsync(trackchanges: false);
            var accountDto = _mapper.Map<IEnumerable<BankAccountDto>>(account);
            return Ok(accountDto);
        }
        //Get bank accounts by id
        //GET:api/bankaccounts/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetBankAccountById(int id)
        {
            var account = await _repositoryManager.BankAccount.GetBankAccountsByIdAsync(id, trackchanges: false);
            if (account == null)
                return NotFound("Bank account not found.");


            var accountDto = _mapper.Map<BankAccountDto>(account);
            return Ok(accountDto);

        }

        //Create bank account
        //POST:api/bankaccounts
        public async Task<IActionResult> CreateBankAccount([FromBody] BankAccountForCreationDto bankAccount)
        {
            if (await _repositoryManager.BankAccount.CodeExistsForClientAsync(bankAccount.Code, bankAccount.ClientId))
                return BadRequest("This code alredy exists.");

            var account = _mapper.Map<BankAccount>(bankAccount);
            _repositoryManager.BankAccount.CreateBankAccount(account);
            await _repositoryManager.SaveAsync();

            return CreatedAtAction(nameof(GetBankAccountById), new { id = account.Id }, _mapper.Map<BankAccountForCreationDto>(account));
        }

        //Update bank account
        //UPDATE:api/bankaccounts/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBankAccount(int id, [FromBody] BankAccountForUpdateDto bankAccount)
        {
            var account = await _repositoryManager.BankAccount.GetBankAccountsByIdAsync(id, trackchanges: true);

            if (account == null)
                return NotFound("Bank account not found.");

            _mapper.Map(bankAccount, account);
            _repositoryManager.BankAccount.UpdateBankAccount(account);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        //Delete bank account
        //DELETE:api/bankaccounts/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var account = await _repositoryManager.BankAccount.GetBankAccountsByIdAsync(id, trackchanges: true);
            if (account == null)
                return NotFound("Bank account not found.");

            account.IsActtive = false;
            _repositoryManager.BankAccount.UpdateBankAccount(account);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }
    }
}
