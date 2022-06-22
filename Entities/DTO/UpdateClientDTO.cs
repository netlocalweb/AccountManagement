using System;

namespace Entities.DTO
{
    public class UpdateClientDTO
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }


    }
}
