using Microsoft.EntityFrameworkCore.Query;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Entities.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        private string code;

        [Required]
        [MaxLength(20)]
        public string Code
        {
            get => code;
            set => code = value?.ToUpper(); 
        }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime? DateModified { get; set; } = DateTime.UtcNow;



    }
}
