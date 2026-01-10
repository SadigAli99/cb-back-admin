using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Social
{
    public class SocialEditDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Icon { get; set; }
        public IFormFile? File { get; set; }
        public string? Url { get; set; }
    }
}
