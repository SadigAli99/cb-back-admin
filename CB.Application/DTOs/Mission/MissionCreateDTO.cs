

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Mission
{
    public class MissionCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Texts { get; set; } = new();
    }
}
