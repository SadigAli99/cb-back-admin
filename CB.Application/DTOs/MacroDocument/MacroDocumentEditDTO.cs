
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.MacroDocument
{
    public class MacroDocumentEditDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Texts { get; set; } = new();
    }
}
