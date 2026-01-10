
namespace CB.Application.DTOs.MoneySignHistoryFeature
{
    public class MoneySignHistoryFeatureGetDTO
    {
        public string? MoneySignHistoryTitle { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
