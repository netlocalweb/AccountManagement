using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/client/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ClientController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _repository.ClientRepository.GetAllClientsAsync();

            var clientsDto = clients.Select(client => new ClientDTO
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Birthdate = client.Birthdate,
                Phone = client.Phone,
                DateCreated = client.DateCreated,
                DateModified = client.DateModified,
                Username = client.Username,
                IsActive = client.IsActive
            });

            return Ok(clientsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _repository.ClientRepository.GetClientByIdAsync(id);

            if (client == null)
                return NotFound("Client not found.");

            var clientDto = new ClientDTO
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Birthdate = client.Birthdate,
                Phone = client.Phone,
                DateCreated = client.DateCreated,
                DateModified = client.DateModified,
                Username = client.Username,
                IsActive = client.IsActive
            };

            return Ok(clientDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDTO clientDto)
        {
            if (clientDto == null)
                return BadRequest("Client data is null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!IsPasswordValid(clientDto.Password))
            {
                return BadRequest("Password must contain at least 8 characters, one lowercase letter, one uppercase letter, one number and one special character.");
            }

            var emailExists = await _repository.ClientRepository.GetClientByEmailAsync(clientDto.Email);
            if (emailExists != null)
                return BadRequest("Email already exists.");

            var phoneExists = await _repository.ClientRepository.GetClientByPhoneAsync(clientDto.Phone);
            if (phoneExists != null)
                return BadRequest("Phone already exists.");

            var usernameExists = await _repository.ClientRepository.GetClientByUsernameAsync(clientDto.Username);
            if (usernameExists != null)
                return BadRequest("Username already exists.");

            var client = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Birthdate = clientDto.Birthdate,
                Phone = clientDto.Phone,
                Username = clientDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(clientDto.Password),
                DateCreated = DateTime.Now,
                IsActive = true
            };

            _repository.ClientRepository.CreateClient(client);
            await _repository.SaveAsync();

            _logger.LogInfo("Client created successfully.");

            var createdClientDto = new ClientDTO
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Birthdate = client.Birthdate,
                Phone = client.Phone,
                DateCreated = client.DateCreated,
                DateModified = client.DateModified,
                Username = client.Username,
                IsActive = client.IsActive
            };

            return Ok(createdClientDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] UpdateClientDTO clientDto)
        {
            if (clientDto == null)
                return BadRequest("Client data is null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = await _repository.ClientRepository.GetClientByIdAsync(id);

            if (client == null)
                return NotFound("Client not found.");

            var emailExists = await _repository.ClientRepository.GetClientByEmailAsync(clientDto.Email);
            if (emailExists != null && emailExists.Id != id)
                return BadRequest("Email already exists.");

            var phoneExists = await _repository.ClientRepository.GetClientByPhoneAsync(clientDto.Phone);
            if (phoneExists != null && phoneExists.Id != id)
                return BadRequest("Phone already exists.");

            var usernameExists = await _repository.ClientRepository.GetClientByUsernameAsync(clientDto.Username);
            if (usernameExists != null && usernameExists.Id != id)
                return BadRequest("Username already exists.");

            client.FirstName = clientDto.FirstName;
            client.LastName = clientDto.LastName;
            client.Email = clientDto.Email;
            client.Birthdate = clientDto.Birthdate;
            client.Phone = clientDto.Phone;
            client.Username = clientDto.Username;
            client.DateModified = DateTime.Now;

            _repository.ClientRepository.UpdateClient(client);
            await _repository.SaveAsync();

            _logger.LogInfo("Client updated successfully.");

            var updatedClientDto = new ClientDTO
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Birthdate = client.Birthdate,
                Phone = client.Phone,
                DateCreated = client.DateCreated,
                DateModified = client.DateModified,
                Username = client.Username,
                IsActive = client.IsActive
            };

            return Ok(updatedClientDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _repository.ClientRepository.GetClientByIdAsync(id);

            if (client == null)
                return NotFound("Client not found.");

            _repository.ClientRepository.DeleteClient(client);
            await _repository.SaveAsync();

            _logger.LogInfo("Client deleted successfully.");

            return Ok("Client deleted successfully.");
        }

        private bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");

            return regex.IsMatch(password);
        }
    }
}