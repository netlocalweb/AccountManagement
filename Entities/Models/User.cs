using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsLockedOut { get; set; } = false;
        public DateTime? LockoutDate { get; set; } = DateTime.UtcNow;
        public Client? Client { get; set; }

    }
}
