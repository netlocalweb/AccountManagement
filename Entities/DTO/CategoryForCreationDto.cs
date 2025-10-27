using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
