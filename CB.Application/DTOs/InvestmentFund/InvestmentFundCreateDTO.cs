

namespace CB.Application.DTOs.InvestmentFund
{
    public class InvestmentFundCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
