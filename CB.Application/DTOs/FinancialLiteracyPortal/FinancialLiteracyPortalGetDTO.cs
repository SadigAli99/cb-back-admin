
namespace CB.Application.DTOs.FinancialLiteracyPortal
{
    public class FinancialLiteracyPortalGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
        public List<FinancialLiteracyPortalImageDTO> Images { get; set; } = new();
    }
}
