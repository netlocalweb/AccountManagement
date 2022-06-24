using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AccountManagement.Controllers
{
    [Route("api/authentication/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IDapperRepository _dapperRepository;

        public AuthController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _dapperRepository = dapperRepository;
        }

        //POST: Register
        [HttpPost("Register")]
        public IActionResult Register([FromBody] CreateClientDTO createClientDto)
        {
           // var client = new Clients(createClientDto.FirstName, createClientDto.LastName, createClientDto.Email, createClientDto.Birthdate,
               // createClientDto.Phone, createClientDto.Username, createClientDto.Password);

            _repository.ClientsRepository.Register(createClientDto, out string ErrorMessage);
            _repository.ClientsRepository.SaveChanges();

            _logger.LogInfo("Registerin a new client");

            return Ok(ErrorMessage);
        }

        //POST: Login
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] RegisterDTO register)
        {
            _repository.ClientsRepository.LoginValidation(register.Username, register.Password, out string ErrorMessage, out Clients client);
            _logger.LogInfo("Client Login");
            if (client != null)
            {
                string token = CreateToken(client);
                return Ok(token);
            }
            else
            {
                return Ok(ErrorMessage);
            }
        }

        //Crete Token Method - valid for 30 minutes
        private string CreateToken(Clients client)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, client.Username),
                new Claim("Id", client.Id.ToString()),
                new Claim(ClaimTypes.Email, client.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("AccountManagementTokenKey"));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
