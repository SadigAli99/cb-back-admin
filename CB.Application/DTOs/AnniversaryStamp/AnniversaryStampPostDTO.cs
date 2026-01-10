using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.AnniversaryStamp
{
    public class AnniversaryStampPostDTO
    {
        public string? Image { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
