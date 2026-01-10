using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.FinancialSearchSystemCaption
{
    public class FinancialSearchSystemCaptionPostDTO
    {
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
