using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILogger<BankTransactionController> _logger;

        public BankTransactionController( IRepositoryManager repositoryManager,IMapper mapper,ILogger<BankTransactionController> logger)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _logger = logger;
        }

        //Get all bank transactions
        //GET:api/banktransactions
        [HttpGet]
        public async Task<IActionResult> GetAllBankTransactions()
        {
           var transactions = await _repositoryManager.BankTransaction.GetAllBankTransactionAsync(trackchanges : false);
            var accountDto = _mapper.Map<IEnumerable<BankTransactionDto>>(transactions);
            return Ok(accountDto);
        }
        //Create  new bank transaction and update the balance
        //POST:api/banktransaction
        [HttpPost]
        public async Task<IActionResult> CreateBankTransaction([FromBody] BankTransactionForCreation bankTransactionDto)
        {
            var transaction = _mapper.Map<BankTransaction>(bankTransactionDto);

            var account = await _repositoryManager.BankAccount
                .GetBankAccountsByIdAsync(bankTransactionDto.BankAccountId, trackchanges: false);

            if (account == null)
            {
                return NotFound("Bank account not found.");
            }

            //if (!account.IsActive)
            //    return BadRequest("Bank account is inactive. Cannot perform transactions.");

            //Update account balance
            if (bankTransactionDto.Action == TransactionAction.Depozitim)
            {
                account.Balance += bankTransactionDto.Amount;
            }
            else if (bankTransactionDto.Action == TransactionAction.Terheqje)
            {
                if (account.Balance < bankTransactionDto.Amount)
                    return BadRequest("Insufficient funds for withdrawal.");

                account.Balance -= bankTransactionDto.Amount;
            }
            else
            {
                return BadRequest("Invalid transaction action.");
            }
            //update modificationd date and save transaction
            account.DateModified = DateTime.UtcNow;

            _repositoryManager.BankTransaction.CreateBankTransaction(transaction);
            await _repositoryManager.SaveAsync();

            var transactionToReturn = _mapper.Map<BankTransactionDto>(transaction);
            return CreatedAtAction(nameof(GetAllBankTransactions), new { id = transactionToReturn.Id }, transactionToReturn);
        }
    }
}
