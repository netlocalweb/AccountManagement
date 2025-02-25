using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Entities.DTO;
using System.Collections.Generic;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public BankAccountController(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllBankAccounts()
        {
            var bankAccounts = _bankAccountRepository.FindAll();
            var bankAccountDTOs = _mapper.Map<IEnumerable<BankAccountDTO>>(bankAccounts);
            return Ok(bankAccountDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBankAccountById(int id)
        {
            var bankAccount = _bankAccountRepository.FindById(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            var bankAccountDTO = _mapper.Map<BankAccountDTO>(bankAccount);
            return Ok(bankAccountDTO);
        }

        [HttpPost]
        public IActionResult AddBankAccount([FromBody] CreateBankAccDTO bankAccountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bankAccountEntity = _mapper.Map<BankAccount>(bankAccountDTO);
            _bankAccountRepository.Create(bankAccountEntity);

            var createdAccountDTO = _mapper.Map<BankAccountDTO>(bankAccountEntity);
            return CreatedAtAction(nameof(GetBankAccountById), new { id = createdAccountDTO.Id }, createdAccountDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBankAccount(int id, [FromBody] CreateBankAccDTO bankAccountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bankAccount = _bankAccountRepository.FindById(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            _mapper.Map(bankAccountDTO, bankAccount);
            _bankAccountRepository.Update(bankAccount);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult SoftDeleteBankAccount(int id)
        {
            var bankAccount = _bankAccountRepository.FindById(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            _bankAccountRepository.SoftDelete(bankAccount);
            return NoContent();
        }
    }
}
