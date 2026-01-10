
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristicImage
{
    public class CurrencyHistoryPrevItemCharacteristicImageEditDTO
    {
        public int Id { get; set; }
        public int CurrencyHistoryPrevId { get; set; }
        public string? Nominal { get; set; }
        public IFormFile? FrontFile { get; set; }
        public IFormFile? BackFile { get; set; }
        public Dictionary<string, string> Colors { get; set; } = new();
        public Dictionary<string, string> Sizes { get; set; } = new();
    }
}
