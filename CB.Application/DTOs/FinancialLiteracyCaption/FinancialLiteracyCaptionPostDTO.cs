

namespace CB.Application.DTOs.FinancialLiteracyCaption
{
    public class FinancialLiteracyCaptionPostDTO
    {
        public string? Url { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
