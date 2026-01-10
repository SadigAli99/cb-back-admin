

namespace CB.Application.DTOs.CoinMoneySignHistory
{
    public class CoinMoneySignHistoryCreateDTO
    {
        public int MoneySignId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
