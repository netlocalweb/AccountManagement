using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Client")]
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool IsActive { get; set; } = true;
    }
}