using AutoMapper;
using Contracts;
using Entities.DTO;
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
        public BankAccountController(IRepositoryManager repositoryManager,IMapper mapper,ILogger<BankAccountController> logger)
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
            var account = await _repositoryManager.BankAccount.GetBankAccountsByIdAsync( id ,trackchanges: false);
            if (account == null)
                return NotFound("Bank account not found.");

            var accountDto = _mapper.Map<BankAccountDto>(account);
            return Ok(accountDto);

        }

        //Create bank account 
        //POST:api/bankaccount
        //[HttpPost]
        //public async Task<IActionResult> CreateBankAccount([FromBody] BankAccountForCreationDto bankaccount)
        //{
        //    if (bankaccount == null)
        //        return BadRequest("Bank account is null");

        //    //Kontroll nese ekziston nje llogari me te jejtin cod per te njejtin klient 
        //    var existingAccount = await _repositoryManager.BankAccount
        //        .FindByCondition(a => a.ClientId == bankaccount.ClientId && a.Code == bankaccount.Code, trackChanges: false)
        //        .FirstOrDefaultAsync();
        //    if (existingAccount != null)
        //        return BadRequest("A bank account with this code alredy exists for the same client.");

        //    _repositoryManager.BankAccount.CreateBankAccount();

        //}


    }
}
