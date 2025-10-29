using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class ClientCreateDto
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
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