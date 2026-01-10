
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.EventContent
{
    public class EventContentEditDTO
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public List<IFormFile> ImageFiles { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
