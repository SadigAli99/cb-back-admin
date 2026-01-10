

namespace CB.Application.DTOs.OutOfBankNoteMoneySignHistory
{
    public class OutOfBankNoteMoneySignHistoryCreateDTO
    {
        public int MoneySignId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
