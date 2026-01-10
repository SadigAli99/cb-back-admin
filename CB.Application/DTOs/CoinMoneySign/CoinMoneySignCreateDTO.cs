
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CoinMoneySign
{
    public class CoinMoneySignCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
