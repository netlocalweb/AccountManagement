using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime Brirthdate { get; set; }
       
        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }


        [Required]
        public required string UserId { get; set; }

        // Navigation properties
        public virtual User User { get; set; } = null!;

    }
}
