
namespace CB.Application.DTOs.MoneySignHistory
{
    public class MoneySignHistoryGetDTO
    {
        public int Id { get; set; }
        public string? Video { get; set; }
        public string? MoneySignTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
