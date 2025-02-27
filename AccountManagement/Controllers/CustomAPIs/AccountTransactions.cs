using AutoMapper;
using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Controllers.CustomAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactions : ControllerBase
    {
        private readonly IBankAccountRepository _bankAccRepo;
        private readonly IBankTransactionRepository _bankTransRepo;
        private readonly IMapper _mapper;

        public AccountTransactions(
            IBankAccountRepository bankAccRepo,
            IBankTransactionRepository bankTransRepo,
            IMapper mapper)
        {
            _bankAccRepo = bankAccRepo;
            _bankTransRepo = bankTransRepo;
            _mapper = mapper;
        }

        [HttpGet("transactionsinfo/{id}")]
        public IActionResult GetBankTransactionsInfo(int id)
        {
            var acc = _bankAccRepo.FindById(id);

            if (acc == null)
            {
                return NotFound($"The Account with ID {id} not found.");
            }

            var transactions = _bankTransRepo.FindByBankAccountId(id);

            if (transactions == null || !transactions.Any())
            {
                return NotFound($"No transactions found for this account.");
            }

            var transactionsDto = _mapper.Map<List<BankTransactionDTO>>(transactions);

            return Ok(transactionsDto);
        }
    }
}
