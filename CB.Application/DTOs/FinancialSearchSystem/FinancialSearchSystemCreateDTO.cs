

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.FinancialSearchSystem
{
    public class FinancialSearchSystemCreateDTO
    {
        public List<IFormFile> ImageFiles { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
