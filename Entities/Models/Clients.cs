using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Clients
    {


        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Firstname")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Lastname")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Birthdate")]
        public DateTime Birthdate { get; set; }
        [Required(ErrorMessage = "Enter Phone Number")]
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        [Required(ErrorMessage = "Enter Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public Clients()
        {

        }

        public Clients(string FirstName, string LastName, string Email, DateTime Birthdate, string Phone, string Username, string Password)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Birthdate = Birthdate;
            this.Phone = Phone;
            this.DateCreated = DateTime.Now;
            this.Username = Username;
            this.Password = Password;


        }


    }
}
