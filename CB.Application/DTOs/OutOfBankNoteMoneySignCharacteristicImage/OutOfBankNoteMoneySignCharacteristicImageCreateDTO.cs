
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.OutOfBankNoteMoneySignCharacteristicImage
{
    public class OutOfBankNoteMoneySignCharacteristicImageCreateDTO
    {
        public int MoneySignHistoryId { get; set; }
        public string? Nominal { get; set; }
        public IFormFile FrontFile { get; set; } = null!;
        public IFormFile BackFile { get; set; } = null!;
        public Dictionary<string, string> Colors { get; set; } = new();
        public Dictionary<string, string> Sizes { get; set; } = new();
    }
}
