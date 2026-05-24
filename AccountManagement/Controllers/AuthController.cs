using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace AccountManagement.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly IConfiguration _configuration;

        public AuthController(RepositoryContext repositoryContext, IConfiguration configuration)
        {
            _repositoryContext = repositoryContext;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Login data is null.");

            var client = await _repositoryContext.Clients
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Username == loginDto.Username);

            if (client == null)
                return Unauthorized("Invalid credentials.");

            if (!VerifyPassword(loginDto.Password, client.Password, client.PasswordSalt))
                return Unauthorized("Invalid credentials.");

            var token = GenerateToken(client.Id, client.Email, client.Username);

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Register data is null.");

            if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
                return BadRequest("Username and password are required.");

            if (!IsValidPassword(loginDto.Password))
                return BadRequest("Password does not meet complexity requirements.");

            var exists = await _repositoryContext.Clients
                .AnyAsync(c => c.Username == loginDto.Username);

            if (exists)
                return Conflict("Username already exists.");

            var salt = GenerateSalt();
            var hashedPassword = HashPassword(loginDto.Password, salt);

            var client = new Client
            {
                Username = loginDto.Username,
                Password = hashedPassword,
                PasswordSalt = salt,
                Email = string.Empty,
                FirstName = string.Empty,
                LastName = string.Empty,
                Phone = string.Empty,
                Birthdate = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            };

            _repositoryContext.Clients.Add(client);
            await _repositoryContext.SaveChangesAsync();

            var token = GenerateToken(client.Id, client.Email, client.Username);

            return Ok(new { token });
        }

        private string GenerateToken(int id, string email, string username)
        {
            var claims = new[]
            {
                new Claim("Id", id.ToString()),
                new Claim("Email", email),
                new Claim("Username", username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            using var derive = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
            var hash = Convert.ToBase64String(derive.GetBytes(32));
            return hash == storedHash;
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
