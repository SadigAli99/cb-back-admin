
namespace CB.Application.DTOs.LotteryVideo
{
    public class LotteryVideoGetDTO
    {
        public int Id { get; set; }
        public string? LotteryTitle { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
