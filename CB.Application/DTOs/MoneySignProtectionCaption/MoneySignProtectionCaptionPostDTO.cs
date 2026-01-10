

namespace CB.Application.DTOs.MoneySignProtectionCaption
{
    public class MoneySignProtectionCaptionPostDTO
    {
        public int MoneySignHistoryId { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
