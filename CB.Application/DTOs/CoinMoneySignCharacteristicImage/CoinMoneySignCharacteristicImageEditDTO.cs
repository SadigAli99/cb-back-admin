
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CoinMoneySignCharacteristicImage
{
    public class CoinMoneySignCharacteristicImageEditDTO
    {
        public int Id { get; set; }
        public int MoneySignHistoryId { get; set; }
        public IFormFile? FrontFile { get; set; }
        public IFormFile? BackFile { get; set; }
    }
}
