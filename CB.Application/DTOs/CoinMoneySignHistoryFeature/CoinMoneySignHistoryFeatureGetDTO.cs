
namespace CB.Application.DTOs.CoinMoneySignHistoryFeature
{
    public class CoinMoneySignHistoryFeatureGetDTO
    {
        public string? MoneySignHistoryTitle { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
