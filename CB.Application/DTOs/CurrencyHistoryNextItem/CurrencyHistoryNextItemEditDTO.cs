
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CurrencyHistoryNextItem
{
    public class CurrencyHistoryNextItemEditDTO
    {
        public int Id { get; set; }
        public int CurrencyHistoryNextId { get; set; }
        public IFormFile? FrontFile { get; set; }
        public IFormFile? BackFile { get; set; }
    }
}
