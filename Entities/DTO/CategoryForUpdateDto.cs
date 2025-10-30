using System.Text.Json.Serialization;

namespace Entities.DTO
{
    public class CategoryForUpdateDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? DateModified { get; set; }
    }
}

