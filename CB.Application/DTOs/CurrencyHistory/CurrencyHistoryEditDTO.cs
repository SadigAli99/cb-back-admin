

namespace CB.Application.DTOs.CurrencyHistory
{
    public class CurrencyHistoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
