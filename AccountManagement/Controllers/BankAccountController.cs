using AccountManagement.API.Models;
using AccountManagement.API.Repositories;
using AccountManagement.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountRepository repository;
        public BankAccountController(IBankAccountRepository repository)
        {
            this.repository = repository;

        }
        // GET all for a client
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int clientId)
        {
            if (clientId <= 0)
                return BadRequest("ClientId must be provided and greater than zero.");

            var accounts = await repository.GetAllAsync(clientId);
            var dtos = accounts.Select(a => new BankAccountReadDto
            {
                Id = a.Id,
                Code = a.Code,
                Name = a.Name,
                CurrencyId = a.CurrencyId,
                Balance = a.Balance,
                ClientId = a.ClientId,
                IsActive = a.IsActive,
                DateCreated = a.DateCreated,
                DateModified = a.DateModified
            }).ToList();

            return Ok(dtos);
        }

        // GET by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await repository.GetByIdAsync(id);
            if (account == null) return NotFound();

            var dto = new BankAccountReadDto
            {
                Id = account.Id,
                Code = account.Code,
                Name = account.Name,
                CurrencyId = account.CurrencyId,
                Balance = account.Balance,
                ClientId = account.ClientId,
                IsActive = account.IsActive,
                DateCreated = account.DateCreated,
                DateModified = account.DateModified
            };

            return Ok(dto);
        }

        // POST create
        [HttpPost]
        public async Task<IActionResult> Create(BankAccountCreateDto dto)
        {
            var account = new BankAccount
            {
                Code = dto.Code,
                Name = dto.Name,
                CurrencyId = dto.CurrencyId,
                Balance = dto.Balance,
                ClientId = dto.ClientId,
                IsActive = true,
                DateCreated = DateTime.UtcNow
            };

            var created = await repository.AddAsync(account);

            var readDto = new BankAccountReadDto
            {
                Id = created.Id,
                Code = created.Code,
                Name = created.Name,
                CurrencyId = created.CurrencyId,
                Balance = created.Balance,
                ClientId = created.ClientId,
                IsActive = created.IsActive,
                DateCreated = created.DateCreated
            };

            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }

        // PUT update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BankAccountUpdateDto dto)
        {
            var existing = await repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Code = dto.Code;
            existing.Name = dto.Name;
            existing.CurrencyId = dto.CurrencyId;
            existing.IsActive = dto.IsActive;
            if (dto.Balance.HasValue) existing.Balance = dto.Balance.Value;

            var updated = await repository.UpdateAsync(existing);

            var readDto = new BankAccountReadDto
            {
                Id = updated.Id,
                Code = updated.Code,
                Name = updated.Name,
                CurrencyId = updated.CurrencyId,
                Balance = updated.Balance,
                ClientId = updated.ClientId,
                IsActive = updated.IsActive,
                DateCreated = updated.DateCreated,
                DateModified = updated.DateModified
            };

            return Ok(readDto);
        }

        //Soft delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await repository.SoftDeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();


        }
    }
}