
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Statute
{
    public class StatuteEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> SubTitles { get; set; } = new();
    }
}
