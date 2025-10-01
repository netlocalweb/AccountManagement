using System;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Dtos
{
    public class ClientCreateDto
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required] 
        public string LastName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required] 
        public DateTime Birthdate { get; set; }

        [Required] 
        public string Phone { get; set; } = null!;

        [Required] 
        public string Username { get; set; } = null!;

        [Required] 
        public string Password { get; set; } = null!; //password will be validated and hashed
    }
}