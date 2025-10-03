using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Models.DTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        [MaxLength(10)]
        public string Code { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = null!;
    }
}
