using System.ComponentModel.DataAnnotations;

namespace AccountManagement.API.DTOs
{
    public class LoginDto
    {
        [Required] 
        public string Username { get; set; } = null!;

        [Required] 
        public string Password { get; set; } = null!;

       

    }
}
