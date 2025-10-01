using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Permissions;

namespace AccountManagement.API.Models
{
    public class Currency
    {
        public  int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public  string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public  string Description { get; set; }

        [Required]
        public  decimal ExchangeRate { get; set; }

        [Required]
        public  DateTime DateCreated { get; set; }

        public  DateTime? DateModified { get; set; }

    }
}
