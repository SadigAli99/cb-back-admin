

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Office
{
    public class OfficeCreateDTO
    {
        public IFormFile ImageFile { get; set; } = null!;
        public IFormFile StatuteFile { get; set; } = null!;
        public string? Phone { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Chairmen { get; set; } = new();
        public Dictionary<string, string> Addresses { get; set; } = new();
    }
}
