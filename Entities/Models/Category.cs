using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        private string _code;
        public string Code
        {
            get => _code;
            set => _code = value?.ToUpper();
        }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateModified { get; set; } = null;
    }
}
