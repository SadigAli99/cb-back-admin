

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CurrencyHistoryPrev
{
    public class CurrencyHistoryPrevCreateDTO
    {
        public int CurrencyHistoryId { get; set; }
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string,string> SubTitles { get; set; } = new();
    }
}
