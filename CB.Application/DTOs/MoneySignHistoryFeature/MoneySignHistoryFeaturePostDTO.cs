

namespace CB.Application.DTOs.MoneySignHistoryFeature
{
    public class MoneySignHistoryFeaturePostDTO
    {
        public int MoneySignHistoryId { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
