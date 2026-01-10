
namespace CB.Application.DTOs.MoneySignProtectionCaption
{
    public class MoneySignProtectionCaptionGetDTO
    {
        public string? MoneySignHistoryTitle { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
