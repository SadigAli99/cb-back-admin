
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.NationalBankNoteMoneySign
{
    public class NationalBankNoteMoneySignCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
