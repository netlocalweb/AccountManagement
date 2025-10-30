
using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace AccountManagement.Service
{

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User? _user;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        
        public AuthService(UserManager<User> userManager , IMapper mapper, IRepositoryManager repositoryManager, ILoggerManager loggerManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _configuration = configuration;
        }

        //Ben regjistrimin e klientit dhe krijon userin 
        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (result.Succeeded)
            {
                var client = new Client
                {
                    UserId = user.Id,
                    FirstName = userForRegistration.FirstName,
                    LastName = userForRegistration.LastName,
                    Email = userForRegistration.Email,
                    Phone = userForRegistration.Phone,
                    Birthdate = userForRegistration.Birthdate
                };
                _repositoryManager.Client.CreateClient(client);
                await _repositoryManager.SaveAsync();
                _loggerManager.LogInfo($"Client profile created for user {user.UserName}");
            }
            return result;

        }

        //Validon perdoruesin per login
        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
           _user = await _userManager.FindByNameAsync(userForAuth.UserName);

             var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
            if (!result)
                _loggerManager.LogWarn($"{nameof(ValidateUser)} : Authentication failed.Wrong user name or password .");

            if (_user.IsLockedOut)
            {
                _loggerManager.LogWarn($"Authentication failed. User {_user.UserName} is locked out.");
                return false;
            }
            return result;
        }
        //Krijon JWT per userin e loguar 
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        }
        //Merr kredencialet per JWT
        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            return new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
        }
        //Lista e claims per userin
        private async Task <List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, _user.UserName),
                new Claim(ClaimTypes.NameIdentifier, _user.Id),
                new Claim(ClaimTypes.Email, _user.Email)
            };
             return claims;

        }
        //Gjenron token Jwt
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
                (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
                );
            return tokenOptions;
        }

    }
}
