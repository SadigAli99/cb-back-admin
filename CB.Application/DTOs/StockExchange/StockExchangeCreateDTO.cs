

namespace CB.Application.DTOs.StockExchange
{
    public class StockExchangeCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
