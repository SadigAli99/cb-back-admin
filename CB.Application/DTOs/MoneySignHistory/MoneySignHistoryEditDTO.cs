

namespace CB.Application.DTOs.MoneySignHistory
{
    public class MoneySignHistoryEditDTO
    {
        public int Id { get; set; }
        public int MoneySignId { get; set; }
        public string? Video { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
