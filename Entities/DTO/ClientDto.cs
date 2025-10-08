using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public  class ClientDto
    {

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
    }
}
