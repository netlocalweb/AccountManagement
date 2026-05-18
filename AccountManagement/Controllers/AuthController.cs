using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [Route("api/auth/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;

        public AuthController(
            IRepositoryManager repository,
            ILoggerManager logger,
            IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null)
                return BadRequest("Login data is null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = await _repository.ClientRepository.GetClientByUsernameAsync(loginDto.Username);

            if (client == null)
                return Unauthorized("Invalid username or password.");

            if (client.IsActive == false)
                return Unauthorized("Client account is not active.");

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginDto.Password, client.PasswordHash);

            if (!isPasswordCorrect)
                return Unauthorized("Invalid username or password.");

            var token = CreateToken(client.Id, client.Email, client.Username);

            _logger.LogInfo("Client logged in successfully.");

            var response = new LoginResponseDTO
            {
                Id = client.Id,
                Email = client.Email,
                Username = client.Username,
                Token = token
            };

            return Ok(response);
        }

        private string CreateToken(int id, string email, string username)
        {
            var claims = new[]
            {
                new Claim("Id", id.ToString()),
                new Claim("Email", email),
                new Claim("Username", username)
            };

            var tokenKey = _configuration.GetSection("AppSettings:Token").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}