
namespace CB.Application.DTOs.PaperMoney
{
    public class PaperMoneyGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Topics { get; set; } = new();
        public Dictionary<string, string> ReleaseDates { get; set; } = new();
    }
}
