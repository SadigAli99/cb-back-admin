
namespace CB.Application.DTOs.CurrencyHistoryNext
{
    public class CurrencyHistoryNextGetDTO
    {
        public int Id { get; set; }
        public string? CurrencyHistoryTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
