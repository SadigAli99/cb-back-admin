
namespace CB.Application.DTOs.FinancialLiteracyEventCaption
{
    public class FinancialLiteracyEventCaptionGetDTO
    {
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
