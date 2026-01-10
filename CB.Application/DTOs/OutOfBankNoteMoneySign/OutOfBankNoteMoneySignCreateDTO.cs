
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.OutOfBankNoteMoneySign
{
    public class OutOfBankNoteMoneySignCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
