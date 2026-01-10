

namespace CB.Application.DTOs.BankInvestment
{
    public class BankInvestmentCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
