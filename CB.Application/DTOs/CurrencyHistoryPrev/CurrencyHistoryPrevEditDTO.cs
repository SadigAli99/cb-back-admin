

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CurrencyHistoryPrev
{
    public class CurrencyHistoryPrevEditDTO
    {
        public int Id { get; set; }
        public int CurrencyHistoryId { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> SubTitles { get; set; } = new();
    }
}
