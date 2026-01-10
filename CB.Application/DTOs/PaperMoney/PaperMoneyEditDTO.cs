
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.PaperMoney
{
    public class PaperMoneyEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Topics { get; set; } = new();
        public Dictionary<string, string> ReleaseDates { get; set; } = new();
    }
}
