

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Video
{
    public class VideoCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public string? Url { get; set; }
        public DateTime? Date { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
