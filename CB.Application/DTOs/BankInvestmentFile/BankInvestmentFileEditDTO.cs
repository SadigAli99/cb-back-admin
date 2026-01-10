
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.BankInvestmentFile
{
    public class BankInvestmentFileEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
