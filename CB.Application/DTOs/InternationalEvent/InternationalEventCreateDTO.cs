

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.InternationalEvent
{
    public class InternationalEventCreateDTO
    {
        public List<IFormFile> ImageFiles { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
