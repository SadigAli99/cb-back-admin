
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.NakhchivanEvent
{
    public class NakhchivanEventEditDTO
    {
        public int Id { get; set; }
        public List<IFormFile> Files { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
