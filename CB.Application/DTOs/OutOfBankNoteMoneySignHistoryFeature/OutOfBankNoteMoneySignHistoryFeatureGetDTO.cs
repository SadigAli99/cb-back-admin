
namespace CB.Application.DTOs.OutOfBankNoteMoneySignHistoryFeature
{
    public class OutOfBankNoteMoneySignHistoryFeatureGetDTO
    {
        public string? MoneySignHistoryTitle { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
