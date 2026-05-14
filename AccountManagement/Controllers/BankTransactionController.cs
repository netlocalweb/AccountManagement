using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/banktransactions")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public BankTransactionController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBankTransactions()
        {
            var transactions = _repository.BankTransactionRepository.GetBankTransactions();
            var transactionsDto = _mapper.Map<BankTransactionDTO[]>(transactions);

            return Ok(transactionsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetBankTransactionById(int id)
        {
            var transaction = _repository.BankTransactionRepository.GetBankTransactionById(id);

            if (transaction == null)
                return NotFound("Bank transaction not found.");

            var transactionDto = _mapper.Map<BankTransactionDTO>(transaction);

            return Ok(transactionDto);
        }

        [HttpGet("bankaccount/{bankAccountId}")]
        public IActionResult GetBankTransactionsByBankAccountId(int bankAccountId)
        {
            var transactions = _repository.BankTransactionRepository
                .GetBankTransactionsByBankAccountId(bankAccountId);

            var transactionsDto = _mapper.Map<BankTransactionDTO[]>(transactions);

            return Ok(transactionsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankTransaction([FromBody] CreateBankTransactionDTO transactionDto)
        {
            if (transactionDto == null)
                return BadRequest("Bank transaction object is null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (transactionDto.Amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            if (transactionDto.Action != 1 && transactionDto.Action != 2)
                return BadRequest("Action must be 1 for deposit or 2 for withdrawal.");

            var bankAccount = _repository.BankAccountRepository
                .GetBankAccountById(transactionDto.BankAccountId);

            if (bankAccount == null)
                return NotFound("Bank account not found.");

            if (!bankAccount.IsActive)
                return BadRequest("Bank account is not active.");

            decimal updatedBalance;

            if (transactionDto.Action == 1)
            {
                updatedBalance = bankAccount.Balance + transactionDto.Amount;
            }
            else
            {
                if (transactionDto.Amount > bankAccount.Balance)
                    return BadRequest("Insufficient balance.");

                updatedBalance = bankAccount.Balance - transactionDto.Amount;
            }

            bankAccount.Balance = updatedBalance;

            bankAccount.DateModified = DateTime.Now;

            var transaction = _mapper.Map<BankTransaction>(transactionDto);

            transaction.IsActive = true;
            transaction.DateCreated = DateTime.Now;
            transaction.DateModified = null;

            _repository.BankAccountRepository.UpdateBankAccount(bankAccount);
            _repository.BankTransactionRepository.CreateBankTransaction(transaction);

            await _repository.SaveAsync();

            var createdTransaction = _mapper.Map<BankTransactionDTO>(transaction);

            return CreatedAtAction(nameof(GetBankTransactionById), new { id = transaction.Id }, createdTransaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBankTransaction(int id, [FromBody] UpdateBankTransactionDTO transactionDto)
        {
            if (transactionDto == null)
                return BadRequest("Bank transaction object is null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (transactionDto.Amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            if (transactionDto.Action != 1 && transactionDto.Action != 2)
                return BadRequest("Action must be 1 for deposit or 2 for withdrawal.");

            var transaction = _repository.BankTransactionRepository.GetBankTransactionById(id);

            if (transaction == null)
                return NotFound("Bank transaction not found.");

            transaction.Action = transactionDto.Action;
            transaction.Amount = transactionDto.Amount;
            transaction.IsActive = transactionDto.IsActive;
            transaction.DateModified = DateTime.Now;

            _repository.BankTransactionRepository.UpdateBankTransaction(transaction);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankTransaction(int id)
        {
            var transaction = _repository.BankTransactionRepository.GetBankTransactionById(id);

            if (transaction == null)
                return NotFound("Bank transaction not found.");

            _repository.BankTransactionRepository.DeleteBankTransaction(transaction);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
