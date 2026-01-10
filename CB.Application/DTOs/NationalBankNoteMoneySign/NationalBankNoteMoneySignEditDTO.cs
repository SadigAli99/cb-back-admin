
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.NationalBankNoteMoneySign
{
    public class NationalBankNoteMoneySignEditDTO
    {
        public int Id { get; set; }
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
