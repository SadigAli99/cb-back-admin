
namespace CB.Application.DTOs.FinancialLiteracyEvent
{
    public class FinancialLiteracyEventCreateDTO
    {
        public DateTime Date { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
