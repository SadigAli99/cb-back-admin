
namespace CB.Application.DTOs.OutOfBankNoteMoneySignCharacteristicImage
{
    public class OutOfBankNoteMoneySignCharacteristicImageGetDTO
    {
        public int Id { get; set; }
        public string? MoneySignHistoryTitle { get; set; }
        public string? Nominal { get; set; }
        public string? FrontImage { get; set; }
        public string? BackImage { get; set; }
        public Dictionary<string, string> Colors { get; set; } = new();
        public Dictionary<string, string> Sizes { get; set; } = new();
    }
}
