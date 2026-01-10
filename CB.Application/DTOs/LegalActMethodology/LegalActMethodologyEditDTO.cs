
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.LegalActMethodology
{
    public class LegalActMethodologyEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
