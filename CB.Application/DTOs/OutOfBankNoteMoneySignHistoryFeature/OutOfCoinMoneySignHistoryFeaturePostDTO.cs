

namespace CB.Application.DTOs.OutOfBankNoteMoneySignHistoryFeature
{
    public class OutOfBankNoteMoneySignHistoryFeaturePostDTO
    {
        public int MoneySignHistoryId { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
