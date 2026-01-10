
namespace CB.Application.DTOs.FinancialEvent
{
    public class FinancialEventCreateDTO
    {
        public DateTime Date { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
