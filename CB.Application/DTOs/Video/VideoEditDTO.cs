
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Video
{
    public class VideoEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public string? Url { get; set; }
        public DateTime? Date { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
