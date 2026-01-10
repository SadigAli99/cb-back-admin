


namespace CB.Application.DTOs.CurrencyHistory
{
    public class CurrencyHistoryGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
