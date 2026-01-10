

namespace CB.Application.DTOs.LotteryFAQ
{
    public class LotteryFAQEditDTO
    {
        public int Id { get; set; }
        public int LotteryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
