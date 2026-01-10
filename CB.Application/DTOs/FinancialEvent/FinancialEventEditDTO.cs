

namespace CB.Application.DTOs.FinancialEvent
{
    public class FinancialEventEditDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
