using Microsoft.EntityFrameworkCore.Query;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace AccountManagement.API.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }



    }
}
