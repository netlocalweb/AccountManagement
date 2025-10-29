using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class ClientUpdateDto
    {
        [Required] 
        public int Id { get; set; }

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

        // Optional password change
        public string? NewPassword { get; set; }
    }
}