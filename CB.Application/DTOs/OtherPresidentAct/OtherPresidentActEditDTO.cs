
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.OtherPresidentAct
{
    public class OtherPresidentActEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
