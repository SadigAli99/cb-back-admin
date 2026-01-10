

namespace CB.Application.DTOs.OutOfBankNoteMoneySignHistory
{
    public class OutOfBankNoteMoneySignHistoryEditDTO
    {
        public int Id { get; set; }
        public int MoneySignId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
