using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.FinancialDevelopment
{
    public class FinancialDevelopmentPostDTO
    {
        public IFormFile? File { get; set; }
        public IFormFile? ImageFile { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
        public Dictionary<string, string> FileHeadTitles { get; set; } = new();
        public Dictionary<string, string> FileTitles { get; set; } = new();
    }

}
