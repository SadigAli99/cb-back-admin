
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.OutOfBankNoteMoneySignCharacteristicImage
{
    public class OutOfBankNoteMoneySignCharacteristicImageEditDTO
    {
        public int Id { get; set; }
        public int MoneySignHistoryId { get; set; }
        public string? Nominal { get; set; }
        public IFormFile? FrontFile { get; set; }
        public IFormFile? BackFile { get; set; }
        public Dictionary<string, string> Colors { get; set; } = new();
        public Dictionary<string, string> Sizes { get; set; } = new();
    }
}
