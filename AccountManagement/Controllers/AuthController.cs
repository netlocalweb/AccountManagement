using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly RepositoryContext _repositoryContext; 

        public AuthController(IConfiguration configuration, RepositoryContext context)
        {
            _configuration = configuration;
            _repositoryContext = context; // Initialize the context
        }

        [HttpPost("register")]
        public async Task<ActionResult<Client>> Register(ClientRegisterDTO request)
        {
            // Check if username or email already exists
            if (_repositoryContext.Clients.Any(c => c.Username == request.Username || c.Email == request.Email))
            {
                return BadRequest("Username or Email already taken.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Create the client object
            var client = new Client
            {
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Birthdate = request.Birthdate,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            // Add the client to the database and save changes
            _repositoryContext.Clients.Add(client);
            await _repositoryContext.SaveChangesAsync();

            // Return the created client
            return Ok(client);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(ClientLoginDTO request)
        {
            var client = _repositoryContext.Clients.SingleOrDefault(c => c.Username == request.Username);
            if (client == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, client.PasswordHash, client.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(client);
            return Ok(token);
        }

        private string CreateToken(Client client)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
                new Claim(ClaimTypes.Name, client.Username),
                new Claim(ClaimTypes.Email, client.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
