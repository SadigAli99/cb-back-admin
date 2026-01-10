
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.LegalBasis
{
    public class LegalBasisCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
