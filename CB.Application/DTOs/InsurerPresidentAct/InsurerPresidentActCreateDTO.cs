
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.InsurerPresidentAct
{
    public class InsurerPresidentActCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
