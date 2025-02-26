using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly IBankTransactionRepository _bankTrans;
        private readonly IBankAccountRepository _bankAcc; 
        private readonly IMapper _mapper;

        public BankTransactionController(
            IBankTransactionRepository bankTrans,
            IBankAccountRepository bankAcc,
            IMapper mapper)
        {
            _bankTrans = bankTrans;
            _bankAcc = bankAcc;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllBankTransactions()
        {
            var bankTransactions = _bankTrans.FindAll();
            var bankTransactionDTOs = _mapper.Map<IEnumerable<BankTransactionDTO>>(bankTransactions);
            return Ok(bankTransactionDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBankTransactionById(int id)
        {
            var bankTransaction = _bankTrans.FindById(id);
            if (bankTransaction == null)
            {
                return NotFound();
            }

            var bankTransactionDTO = _mapper.Map<BankTransactionDTO>(bankTransaction);
            return Ok(bankTransactionDTO);
        }

        [HttpPost]
        public IActionResult AddBankTransaction([FromBody] BankTransactionDTO bankTransactionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bankTransactionEntity = _mapper.Map<BankTransaction>(bankTransactionDTO);

            var bankAccount = _bankAcc.FindById(bankTransactionEntity.BankAccountId);
            if (bankAccount == null)
            {
                return NotFound($"Bank account with ID {bankTransactionEntity.BankAccountId} not found.");
            }

            // updating the bank account balance
            if (bankTransactionEntity.Action == 1) // Deposit
            {
                bankAccount.Balance += bankTransactionEntity.Amount;
            }
            else if (bankTransactionEntity.Action == 2) // Withdrawal
            {
                if (bankAccount.Balance < bankTransactionEntity.Amount)
                {
                    return BadRequest("Insufficient balance for withdrawal.");
                }
                bankAccount.Balance -= bankTransactionEntity.Amount;
            }
            else
            {
                return BadRequest("Invalid action specified.");
            }
            _bankAcc.Update(bankAccount);

            _bankTrans.Create(bankTransactionEntity);
            var createdTransactionDTO = _mapper.Map<BankTransactionDTO>(bankTransactionEntity);
            return CreatedAtAction(nameof(GetBankTransactionById), new { id = createdTransactionDTO.Id }, createdTransactionDTO);
        }
    }
}
