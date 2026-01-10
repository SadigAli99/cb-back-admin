
namespace CB.Application.DTOs.FinancialLiteracyCaption
{
    public class FinancialLiteracyCaptionGetDTO
    {
        public string? Url { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
