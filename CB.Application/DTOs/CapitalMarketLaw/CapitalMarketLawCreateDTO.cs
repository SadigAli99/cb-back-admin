
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CapitalMarketLaw
{
    public class CapitalMarketLawCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
