


namespace CB.Application.DTOs.LotteryVideo
{
    public class LotteryVideoCreateDTO
    {
        public int LotteryId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
