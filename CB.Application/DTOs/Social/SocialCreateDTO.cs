using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Social
{
    public class SocialCreateDTO
    {
        public string? Title { get; set; }
        public string? Icon { get; set; }
        public IFormFile File { get; set; } = null!;
        public string? Url { get; set; }
    }
}
