using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }

    public class AddCategoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
