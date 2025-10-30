using System.Text.Json.Serialization;

namespace Entities.DTO
{
    public class CategoryForCreationDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime DateCreated { get; set; }

    }
}
