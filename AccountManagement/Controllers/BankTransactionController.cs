using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private readonly IBankTransactionRepository _bankTransactionRepository;
        private readonly IMapper _mapper; 

        public BankTransactionController(IBankTransactionRepository bankTransactionRepository, IMapper mapper)
        {
            _bankTransactionRepository = bankTransactionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllBankTransactions()
        {
            var bankTransactions = _bankTransactionRepository.FindAll();
            var bankTransactionDTOs = _mapper.Map<IEnumerable<BankTransactionDTO>>(bankTransactions);
            return Ok(bankTransactionDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBankTransactionById(int id)
        {
            var bankTransaction = _bankTransactionRepository.FindById(id);
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
            _bankTransactionRepository.Create(bankTransactionEntity);

            var createdTransactionDTO = _mapper.Map<BankTransactionDTO>(bankTransactionEntity);
            return CreatedAtAction(nameof(GetBankTransactionById), new { id = createdTransactionDTO.Id }, createdTransactionDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBankTransaction(int id, [FromBody] BankTransactionDTO bankTransactionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bankTransaction = _bankTransactionRepository.FindById(id);
            if (bankTransaction == null)
            {
                return NotFound();
            }

            _mapper.Map(bankTransactionDTO, bankTransaction);
            _bankTransactionRepository.Update(bankTransaction);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult SoftDeleteBankTransaction(int id)
        {
            var bankTransaction = _bankTransactionRepository.FindById(id);
            if (bankTransaction == null)
            {
                return NotFound();
            }

            _bankTransactionRepository.SoftDelete(bankTransaction);
            return NoContent();
        }
    }
}
