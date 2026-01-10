
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.OutOfCoinMoneySign
{
    public class OutOfCoinMoneySignEditDTO
    {
        public int Id { get; set; }
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
