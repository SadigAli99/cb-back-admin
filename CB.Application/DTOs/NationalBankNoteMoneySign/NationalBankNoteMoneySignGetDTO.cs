
namespace CB.Application.DTOs.NationalBankNoteMoneySign
{
    public class NationalBankNoteMoneySignGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
