
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Statute
{
    public class StatuteCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> SubTitles { get; set; } = new();
    }
}
