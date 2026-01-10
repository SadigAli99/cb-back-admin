
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.OtherPresidentAct
{
    public class OtherPresidentActCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
