

namespace CB.Application.DTOs.CurrencyHistoryPrevItem
{
    public class CurrencyHistoryPrevItemEditDTO
    {
        public int Id { get; set; }
        public int CurrencyHistoryPrevId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
