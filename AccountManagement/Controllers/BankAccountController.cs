using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountRepository repository;
        private readonly IMapper mapper;
        public BankAccountController(IBankAccountRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;

        }
        // GET all for only logged in clients 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientId = GetClientIdFromToken();
            if (clientId == null) return Unauthorized();

            var accounts = await repository.GetAllAsync(clientId.Value);
            var dtos = mapper.Map<IEnumerable<BankAccountReadDto>>(accounts);
            return Ok(dtos);
        }
        

        // GET by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await repository.GetByIdAsync(id);
            if (account == null) //checks if account is null
                return NotFound();

            var dto = mapper.Map<BankAccountReadDto>(account);
            return Ok(dto);
        }

        // POST create
        [HttpPost]
        public async Task<IActionResult> Create(BankAccountCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var clientId = GetClientIdFromToken();
            if (clientId == null) return Unauthorized();

            // Map Dto to entity
            var account = mapper.Map<BankAccount>(dto);
            account.ClientId = clientId.Value;
            account.IsActive = true;
            account.DateCreated = DateTime.UtcNow;

            var created = await repository.AddAsync(account);
            var readDto = mapper.Map<BankAccountReadDto>(created);

            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }

        // PUT update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BankAccountUpdateDto dto)
        {
            var existing = await repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            // Code uniqueness 
            var newCode = dto.Code.Trim().ToUpper();
            bool exists = await repository.ExistsByCodeAsync(existing.ClientId, newCode, excludeId: id);
            if (exists)
                return BadRequest($"Client already has another account with code '{dto.Code}'.");

           
            if (dto.Balance < 0)
                return BadRequest("Balance cannot be negative.");

           
            mapper.Map(dto, existing);
            existing.Code = newCode;
            existing.DateModified = DateTime.UtcNow;

            var updated = await repository.UpdateAsync(existing);
            var readDto = mapper.Map<BankAccountReadDto>(updated);
            return Ok(readDto);
        }

        //Soft delete
        [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
            try
            {
                var existing = await repository.GetByIdAsync(id);
                if (existing == null) return NotFound(new { message = "Account not found." });

                // Checks if there is no balance before deleting it
                if (existing.Balance > 0)
                    throw new InvalidOperationException("Cannot delete an account with a positive balance.");

                var deleted = await repository.SoftDeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while deleting the account." });
            }
        }

        //Extra method to get clientId from token
        private int? GetClientIdFromToken()
        {
            var claim = User.FindFirst("ClientId")?.Value;
            if (string.IsNullOrEmpty(claim) || !int.TryParse(claim, out int clientId))
                return null;
            return clientId;
        }
    }
}
