using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.FinancialLiteracyEventCaption
{
    public class FinancialLiteracyEventCaptionPostDTO
    {
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
