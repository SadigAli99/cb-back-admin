
namespace CB.Application.DTOs.Lottery
{
    public class LotteryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Descrtiptions { get; set; } = new();
    }
}
