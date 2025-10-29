using Entities.DTOs;
using Repository;
using Contracts;
using AccountManagement.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        private readonly PasswordHasher<Entities.Models.Client> passHasher;//hashes and verifies password
        private readonly JwtTokenService jwtService;//generates a jwt token after login

        public AuthController(
            IClientRepository clientRepository,
            JwtTokenService jwtService)
        {
            this.clientRepository = clientRepository;
            this.jwtService = jwtService;
            this.passHasher = new PasswordHasher<Entities.Models.Client>();
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

            //Generates a jwt token which is used for 30 min
            var token = jwtService.GenerateToken(client);
            return Ok(new { Token = token });
        }
       
            }
        }

