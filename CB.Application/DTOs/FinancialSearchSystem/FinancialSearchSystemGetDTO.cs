
namespace CB.Application.DTOs.FinancialSearchSystem
{
    public class FinancialSearchSystemGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
        public List<FinancialSearchSystemImageDTO> Images { get; set; } = new();
    }
}
