
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.PaperMoney
{
    public class PaperMoneyCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Topics { get; set; } = new();
        public Dictionary<string, string> ReleaseDates { get; set; } = new();
    }
}
