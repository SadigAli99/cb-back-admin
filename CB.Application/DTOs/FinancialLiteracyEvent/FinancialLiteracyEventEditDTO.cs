

namespace CB.Application.DTOs.FinancialLiteracyEvent
{
    public class FinancialLiteracyEventEditDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
