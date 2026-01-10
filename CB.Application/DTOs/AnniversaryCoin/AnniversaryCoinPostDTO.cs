using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.AnniversaryCoin
{
    public class AnniversaryCoinPostDTO
    {
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
