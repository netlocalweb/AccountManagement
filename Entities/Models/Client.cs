using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public DateTime Birthdate { get; set; }
       
        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }


        [Required]
        public required string UserId { get; set; }

        // Navigation properties
        public virtual User User { get; set; } = null!;

        public ICollection<BankAccount>? BankAccounts { get; set; }

    }
}
