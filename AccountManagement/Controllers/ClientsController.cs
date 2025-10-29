using AccountManagement.Validation;
using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagementControllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        private readonly PasswordHasher<Client> passHasher;
        private readonly IMapper mapper;

        public ClientsController(IClientRepository repo, IMapper mapper)
        {
            this.clientRepository = repo;
            this.mapper = mapper;
            passHasher = new PasswordHasher<Client>();
        }

        // GET: api/clients
        // Get all clients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await clientRepository.GetAllAsync();
            var dtos = mapper.Map<IEnumerable<ClientReadDto>>(clients);
            return Ok(dtos);
        }

        // GET: api/clients/{id}
        // Get clients by id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();

            var dto = mapper.Map<ClientReadDto>(client);
            return Ok(dto);
        }

        // POST: api/clients
        // Create a new client
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Password strength
            if (!PasswordValidator.IsStrong(dto.Password))
                return BadRequest("Password must be at least 8 chars, include upper & lower case letters, a digit and a special character.");

            // Setting the email, phone and username as unique
            if (await clientRepository.GetByEmailAsync(dto.Email) != null)
                return Conflict("Email already in use.");

            if (await clientRepository.GetByPhoneAsync(dto.Phone) != null)
                return Conflict("Phone already in use.");

            if (await clientRepository.GetByUsernameAsync(dto.Username) != null)
                return Conflict("Username already in use.");

            var client = mapper.Map<Client>(dto);

            // Hash password
            client.PasswordHash = passHasher.HashPassword(client, dto.Password);

            await clientRepository.CreateAsync(client);

            var resultDto = mapper.Map<ClientReadDto>(client);

            return CreatedAtAction(nameof(GetById), new { id = client.Id }, resultDto);
        }

        // PUT: api/clients/{id} 
        //Updating an existing clients
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.Id) return BadRequest("Id is not matching.");

            var existing = await clientRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Check unique email/phone/username if changed
            var byEmail = await clientRepository.GetByEmailAsync(dto.Email);
            if (byEmail != null && byEmail.Id != id) return Conflict("Email is already in use.");

            var byPhone = await clientRepository.GetByPhoneAsync(dto.Phone);
            if (byPhone != null && byPhone.Id != id) return Conflict("Phone is already in use.");

            var byUsername = await clientRepository.GetByUsernameAsync(dto.Username);
            if (byUsername != null && byUsername.Id != id) return Conflict("Username is already in use.");

            //Map updated dto to the existing model
            mapper.Map(dto, existing);
            existing.DateModified = DateTime.UtcNow;

            // If new password provided, validate & hash it
            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                if (!PasswordValidator.IsStrong(dto.NewPassword))
                    return BadRequest("New password must match the required strength .");

                existing.PasswordHash = passHasher.HashPassword(existing, dto.NewPassword);
            }

            await clientRepository.UpdateAsync(existing);
            return NoContent();
        }

        // DELETE: api/clients/{id}
        // Deleting a client
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await clientRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await clientRepository.DeleteAsync(existing);
            return NoContent();
        }
    }
}
