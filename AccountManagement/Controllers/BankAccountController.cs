using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AccountManagement.Controllers
{
    [Route("api/bankaccounts")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public BankAccountController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBankAccounts()
        {
            var bankAccounts = _repository.BankAccountRepository.GetBankAccounts();
            var bankAccountsDto = _mapper.Map<BankAccountDTO[]>(bankAccounts);

            return Ok(bankAccountsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetBankAccountById(int id)
        {
            var bankAccount = _repository.BankAccountRepository.GetBankAccountById(id);

            if (bankAccount == null)
                return NotFound("Bank account not found.");

            var bankAccountDto = _mapper.Map<BankAccountDTO>(bankAccount);

            return Ok(bankAccountDto);
        }

        [HttpGet("client/{clientId}")]
        public IActionResult GetBankAccountsByClientId(int clientId)
        {
            var bankAccounts = _repository.BankAccountRepository.GetBankAccountsByClientId(clientId);
            var bankAccountsDto = _mapper.Map<BankAccountDTO[]>(bankAccounts);

            return Ok(bankAccountsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankAccount([FromBody] CreateBankAccountDTO bankAccountDto)
        {
            if (bankAccountDto == null)
                return BadRequest("Bank account object is null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (bankAccountDto.Balance < 0)
                return BadRequest("Balance cannot be negative.");

            var existingBankAccount = _repository.BankAccountRepository
                .GetBankAccountByCodeAndClientId(bankAccountDto.Code, bankAccountDto.ClientId);

            if (existingBankAccount != null)
                return BadRequest("This client already has a bank account with this code.");

            var bankAccount = _mapper.Map<BankAccount>(bankAccountDto);

            bankAccount.IsActive = true;
            bankAccount.DateCreated = DateTime.Now;
            bankAccount.DateModified = null;

            _repository.BankAccountRepository.CreateBankAccount(bankAccount);
            await _repository.SaveAsync();

            var createdBankAccount = _mapper.Map<BankAccountDTO>(bankAccount);

            return CreatedAtAction(nameof(GetBankAccountById), new { id = bankAccount.Id }, createdBankAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBankAccount(int id, [FromBody] UpdateBankAccountDTO bankAccountDto)
        {
            if (bankAccountDto == null)
                return BadRequest("Bank account object is null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (bankAccountDto.Balance < 0)
                return BadRequest("Balance cannot be negative.");

            var bankAccount = _repository.BankAccountRepository.GetBankAccountById(id);

            if (bankAccount == null)
                return NotFound("Bank account not found.");

            var existingBankAccount = _repository.BankAccountRepository
                .GetBankAccountByCodeAndClientId(bankAccountDto.Code, bankAccount.ClientId);

            if (existingBankAccount != null && existingBankAccount.Id != id)
                return BadRequest("This client already has another bank account with this code.");

            bankAccount.Code = bankAccountDto.Code;
            bankAccount.Name = bankAccountDto.Name;
            bankAccount.CurrencyId = bankAccountDto.CurrencyId;
            bankAccount.Balance = bankAccountDto.Balance;
            bankAccount.IsActive = bankAccountDto.IsActive;
            bankAccount.DateModified = DateTime.Now;

            _repository.BankAccountRepository.UpdateBankAccount(bankAccount);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankAccount(int id)
        {
            var bankAccount = _repository.BankAccountRepository.GetBankAccountById(id);

            if (bankAccount == null)
                return NotFound("Bank account not found.");

            bankAccount.IsActive = false;
            bankAccount.DateModified = DateTime.Now;

            _repository.BankAccountRepository.UpdateBankAccount(bankAccount);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}