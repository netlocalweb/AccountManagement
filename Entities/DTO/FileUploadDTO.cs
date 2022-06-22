using Microsoft.AspNetCore.Http;

namespace Entities.DTO
{
    public class FileUploadDTO
    {
        public IFormFile files { get; set; }
    }
}
