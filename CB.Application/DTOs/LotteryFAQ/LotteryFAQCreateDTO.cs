

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.LotteryFAQ
{
    public class LotteryFAQCreateDTO
    {
        public int LotteryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
