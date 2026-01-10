

namespace CB.Application.DTOs.MoneySignHistory
{
    public class MoneySignHistoryCreateDTO
    {
        public int MoneySignId { get; set; }
        public string? Video { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
