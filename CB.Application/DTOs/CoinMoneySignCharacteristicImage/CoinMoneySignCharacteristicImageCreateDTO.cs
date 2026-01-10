
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CoinMoneySignCharacteristicImage
{
    public class CoinMoneySignCharacteristicImageCreateDTO
    {
        public int MoneySignHistoryId { get; set; }
        public IFormFile FrontFile { get; set; } = null!;
        public IFormFile BackFile { get; set; } = null!;
    }
}
