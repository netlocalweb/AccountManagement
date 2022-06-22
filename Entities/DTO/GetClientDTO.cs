using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class GetClientDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }  
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string Username { get; set; }   

        public GetClientDTO() { }

        public GetClientDTO(int id, string firstname, string lastname, string email, string phone, DateTime dateCreated, DateTime? dateModified, string username)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Phone = phone;
            DateCreated = dateCreated;
            DateModified = dateModified;
            Username = username;
        }
    }
}
