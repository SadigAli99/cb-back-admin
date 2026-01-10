
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.NakhchivanEvent
{
    public class NakhchivanEventCreateDTO
    {
        public List<IFormFile> Files { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
