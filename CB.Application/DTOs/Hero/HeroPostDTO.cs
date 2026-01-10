using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Hero
{
    public class HeroPostDTO
    {
        public string? Image { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
