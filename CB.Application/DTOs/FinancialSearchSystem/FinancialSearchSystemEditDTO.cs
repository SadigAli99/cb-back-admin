
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.FinancialSearchSystem
{
    public class FinancialSearchSystemEditDTO
    {
        public int Id { get; set; }
        public List<IFormFile> ImageFiles { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
