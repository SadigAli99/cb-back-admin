
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.OutOfCoinMoneySign
{
    public class OutOfCoinMoneySignCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
