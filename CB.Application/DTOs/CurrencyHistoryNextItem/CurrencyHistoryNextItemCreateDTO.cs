
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CurrencyHistoryNextItem
{
    public class CurrencyHistoryNextItemCreateDTO
    {
        public int CurrencyHistoryNextId { get; set; }
        public IFormFile FrontFile { get; set; } = null!;
        public IFormFile BackFile { get; set; } = null!;
    }
}
