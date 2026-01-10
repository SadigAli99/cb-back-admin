
namespace CB.Application.DTOs.CurrencyHistoryPrevItem
{
    public class CurrencyHistoryPrevItemGetDTO
    {
        public int Id { get; set; }
        public string? CurrencyHistoryPrevTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
