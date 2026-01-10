

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.MacroDocument
{
    public class MacroDocumentCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Texts { get; set; } = new();
    }
}
