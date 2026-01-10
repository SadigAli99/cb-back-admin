
namespace CB.Application.DTOs.CurrencyHistoryNext
{
    public class CurrencyHistoryNextEditDTO
    {
        public int Id { get; set; }
        public int CurrencyHistoryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
