using Entities;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public ClientsController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientForCreationDto clientDto)
        {
            if (clientDto == null)
                return BadRequest("Client data is null.");

            if (!IsValidEmail(clientDto.Email))
                return BadRequest("Email is invalid.");

            if (!IsValidPassword(clientDto.Password))
                return BadRequest("Password does not meet complexity requirements.");

            var exists = await _repositoryContext.Clients.AnyAsync(c =>
                c.Email == clientDto.Email || c.Phone == clientDto.Phone || c.Username == clientDto.Username);

            if (exists)
                return Conflict("Email, phone, or username already exists.");

            var salt = GenerateSalt();
            var hashedPassword = HashPassword(clientDto.Password, salt);

            var client = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Birthdate = clientDto.Birthdate,
                Phone = clientDto.Phone,
                Username = clientDto.Username,
                Password = hashedPassword,
                PasswordSalt = salt,
                DateCreated = DateTime.UtcNow
            };

            _repositoryContext.Clients.Add(client);
            await _repositoryContext.SaveChangesAsync();

            return Ok(new { client.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _repositoryContext.Clients
                .AsNoTracking()
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Birthdate = c.Birthdate,
                    Phone = c.Phone,
                    DateCreated = c.DateCreated,
                    DateModified = c.DateModified,
                    Username = c.Username
                })
                .ToListAsync();

            return Ok(clients);
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var address = new MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            var hasLower = false;
            var hasUpper = false;
            var hasDigit = false;
            var hasSpecial = false;

            foreach (var ch in password)
            {
                if (char.IsLower(ch))
                    hasLower = true;
                else if (char.IsUpper(ch))
                    hasUpper = true;
                else if (char.IsDigit(ch))
                    hasDigit = true;
                else
                    hasSpecial = true;
            }

            return hasLower && hasUpper && hasDigit && hasSpecial;
        }

        private static string GenerateSalt()
        {
            var bytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        private static string HashPassword(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using var derive = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
            return Convert.ToBase64String(derive.GetBytes(32));
        }
    }
}
