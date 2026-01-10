
namespace CB.Application.DTOs.CurrencyHistoryPrev
{
    public class CurrencyHistoryPrevGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? CurrencyHistoryTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> SubTitles { get; set; } = new();
    }
}
