using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AccountManagement.API.Models;
using Microsoft.Extensions.Configuration;

namespace AccountManagement.API.Validation
{


    public class JwtTokenService
    {
        private readonly IConfiguration config;
        public JwtTokenService(IConfiguration config)
        {
            this.config = config;
        }

        public string GenerateToken(Client client)
        {
            var jwt = config.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwt["Key"]);
            var expiryMinutes = Convert.ToDouble(jwt["ExpiryMinutes"]);

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim("Id", client.Id.ToString()),
                new Claim("Email", client.Email),
                new Claim("Username", client.Username)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                Issuer = jwt["Issuer"],
                Audience = jwt["Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
