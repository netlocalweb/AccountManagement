using Contracts;

//my code
using Entities.Models;
using Entities.DTO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
//my code - END
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace AccountManagement.Controllers
{

    [Route("api/test/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        //my code

        public static User user = new User();

        private readonly IConfiguration _configuration;
        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (user.Username != request.Username) 
            {
                return BadRequest("User not found!"); 
            }
            if(!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(.5),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        //my code - END
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IDapperRepository _dapperRepository;
        

        public TestController(IRepositoryManager repository, ILoggerManager logger, IDapperRepository dapperRepository)
        {
            _repository = repository;
            _logger = logger;
            _dapperRepository = dapperRepository;
        }

        /// <summary>
        /// Test Api From Base
        /// </summary>
        /// <returns>Test String</returns>
        [HttpGet(Name = "testfrombase")]
        public IActionResult TestFromBase()
        {
            var testStr = _repository.TestRepository.TestMethodFromBase();

            _logger.LogInfo("test method from base is called");

            return Ok(testStr);
        }

        /// <summary>
        /// Test Api
        /// </summary>
        /// <returns>Test String</returns>
        [HttpGet(Name = "testapi")]
        public IActionResult TestApi()
        {
            var testStr = _repository.TestRepository.TestMethod();

            _logger.LogInfo("test method is called");

            return Ok(testStr);
        }

        [HttpGet("dapper-get-all")]
        public IActionResult DapperGetAll()
        {
            var result = _dapperRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("dapper-get-by-id/{id}")]
        public IActionResult DapperGetById(int id)
        {
            var result = _dapperRepository.GetById(id);
            if (result == null)
                return NotFound($"Entity with id {id} not found");

            return Ok(result);
        }
    }
}