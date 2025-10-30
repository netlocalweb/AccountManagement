using AccountManagement.Service;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<BankAccountController> _logger;
        private readonly IAuthService _authService;
        public BankAccountController(IRepositoryManager repositoryManager, IMapper mapper, ILogger<BankAccountController> logger, IAuthService authService)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
            _authService = authService;
        }

        //Get all bank accounts 
        //GET:api/bankaccounts
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBankAccounts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var client = await _repositoryManager.Client.GetClientByUserIdAsync(userId, false);
            if (client == null)
                return BadRequest("Client dosen't exists");

            var account = await _repositoryManager.BankAccount.GetBankAccountByClientId(client.Id,trackchanges: false);
            var accountDto = _mapper.Map<IEnumerable<BankAccountDto>>(account);
            return Ok(accountDto);
        }
        //Get bank accounts by id
        //GET:api/bankaccounts/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetBankAccountById(int id)
        {
            var account = await _repositoryManager.BankAccount.GetBankAccountByIdAsync(id, trackchanges: false);
            if (account == null)
                return NotFound("Bank account not found.");


            var accountDto = _mapper.Map<BankAccountDto>(account);
            return Ok(accountDto);

        }

        //Create bank account
        //POST:api/bankaccounts
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBankAccount([FromBody] BankAccountForCreationDto bankAccount)
        {
            // Get logged in userId
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var client = _repositoryManager.Client.GetClientByUserIdAsync(userId,false);
            if (client == null)
                return BadRequest("Client dosen't exists");

            var exists = await _repositoryManager.BankAccount.CodeExistsForClientAsync(bankAccount.Code, client.Result.Id);

            if (exists == true)
                return BadRequest("This code alredy exists.");

            bankAccount.ClientId = client.Result.Id;
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
            var account = await _repositoryManager.BankAccount.GetBankAccountByIdAsync(id, trackchanges: true);

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
            var account = await _repositoryManager.BankAccount.GetBankAccountByIdAsync(id, trackchanges: true);
            if (account == null)
                return NotFound("Bank account not found.");

            account.IsActive = false;
            account.DateModified = DateTime.UtcNow;
            _repositoryManager.BankAccount.UpdateBankAccount(account);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }
    }
}
