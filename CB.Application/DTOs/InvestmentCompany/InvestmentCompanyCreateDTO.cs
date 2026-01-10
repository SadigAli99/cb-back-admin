

namespace CB.Application.DTOs.InvestmentCompany
{
    public class InvestmentCompanyCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
