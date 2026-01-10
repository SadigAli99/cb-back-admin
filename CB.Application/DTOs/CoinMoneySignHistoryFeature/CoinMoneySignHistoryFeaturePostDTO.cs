

namespace CB.Application.DTOs.CoinMoneySignHistoryFeature
{
    public class CoinMoneySignHistoryFeaturePostDTO
    {
        public int MoneySignHistoryId { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
