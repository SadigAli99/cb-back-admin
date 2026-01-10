
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CapitalMarketPresidentAct
{
    public class CapitalMarketPresidentActCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
