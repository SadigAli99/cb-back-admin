
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CapitalMarket
{
    public class CapitalMarketCreateDTO
    {
        public int CapitalMarketCategoryId { get; set; }
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
