
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.FinancingActivity
{
    public class FinancingActivityCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
