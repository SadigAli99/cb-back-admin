using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.FinancialLiteracyPortalCaption
{
    public class FinancialLiteracyPortalCaptionPostDTO
    {
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
