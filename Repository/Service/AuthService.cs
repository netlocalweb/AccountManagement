

using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Entities.Models;

namespace AccountManagement.Service
{

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;


        public AuthService(UserManager<User> userManager , IMapper mapper, IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }

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
                    Brirthdate = userForRegistration.Brirthdate
                };
                _repositoryManager.Client.CreateClient(client);
                await _repositoryManager.SaveAsync();
                _loggerManager.LogInfo($"Client profile created for user {user.UserName}");
            }
            return result;

        }
    }
}
