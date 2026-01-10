
namespace CB.Application.DTOs.OutOfBankNoteMoneySign
{
    public class OutOfBankNoteMoneySignGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
