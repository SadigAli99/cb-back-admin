

namespace CB.Application.DTOs.OutOfCoinMoneySignHistoryFeature
{
    public class OutOfCoinMoneySignHistoryFeaturePostDTO
    {
        public int MoneySignHistoryId { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
