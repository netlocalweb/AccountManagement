using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.DTO
{
    public class UpdateClientDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        [JsonIgnore]
        public bool IsLockedOut { get; set; }
    }
}
