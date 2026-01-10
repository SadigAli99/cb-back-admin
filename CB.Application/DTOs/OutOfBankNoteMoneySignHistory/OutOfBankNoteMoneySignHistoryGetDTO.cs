
namespace CB.Application.DTOs.OutOfBankNoteMoneySignHistory
{
    public class OutOfBankNoteMoneySignHistoryGetDTO
    {
        public int Id { get; set; }
        public string? MoneySignTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
