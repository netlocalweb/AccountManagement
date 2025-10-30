using Entities.DTO;
using Microsoft.AspNetCore.Identity;

namespace Contracts
{
    //Autentifikimi dhe regjisrimi i userave
    public interface IAuthService
    {
        //Regjistrimi i nje useri te ri 
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        //Validon kredencialet e userit per login
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        //Krijon token per userin e loguar
        Task<string> CreateToken();
    }
}
