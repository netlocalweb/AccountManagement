using AccountManagement.API.DTOs;
using AccountManagement.API.Models.DTOs;
using AccountManagement.API.Repositories;
using AccountManagement.API.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        private readonly PasswordHasher<Models.Client> passHasher;//hashes and verifies password
        private readonly JwtTokenService jwtService;//generates a jwt token after login

        public AuthController(
            IClientRepository clientRepository,
            JwtTokenService jwtService)
        {
            this.clientRepository = clientRepository;
            this.jwtService = jwtService;
            this.passHasher = new PasswordHasher<Models.Client>();
        }
        //Login method that contains username and password
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//if the login dto is invalid it returns 400 bad request

            //Checks if user exists
            var client = await clientRepository.GetByUsernameAsync(dto.Username);
            if (client == null)
                return Unauthorized("Invalid username or password.");

            //Verifies password
            var result = passHasher.VerifyHashedPassword(client, client.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid username or password.");

            //Generates a jwt token if password matches for authentication
            var token = jwtService.GenerateToken(client);
            return Ok(new { Token = token });
        }

        //Register method
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Check if username or email already exists
                var existingUser = await clientRepository.GetByUsernameAsync(dto.Username);
                if (existingUser != null)
                    return BadRequest("Username is already taken.");

                existingUser = await clientRepository.GetByEmailAsync(dto.Email);
                if (existingUser != null)
                    return BadRequest("Email is already registered.");

                // Create new client
                var client = new Models.Client
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Username = dto.Username,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Birthdate = dto.Birthdate,
                    DateCreated = DateTime.UtcNow
                };

                // Hash the password
                client.PasswordHash = passHasher.HashPassword(client, dto.Password);


                await clientRepository.AddAsync(client);

                return Ok(new { Message = "User was registered successfully." });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new
                {
                    Message = "An error occurred while processing your request.",
                    Details = ex.Message 
                });

            }
        }
    }
}
