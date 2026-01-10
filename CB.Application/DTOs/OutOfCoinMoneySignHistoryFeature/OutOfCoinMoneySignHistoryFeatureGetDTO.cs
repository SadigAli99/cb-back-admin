
namespace CB.Application.DTOs.OutOfCoinMoneySignHistoryFeature
{
    public class OutOfCoinMoneySignHistoryFeatureGetDTO
    {
        public string? MoneySignHistoryTitle { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
