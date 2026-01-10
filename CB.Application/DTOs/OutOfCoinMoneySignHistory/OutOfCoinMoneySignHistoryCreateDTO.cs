

namespace CB.Application.DTOs.OutOfCoinMoneySignHistory
{
    public class OutOfCoinMoneySignHistoryCreateDTO
    {
        public int MoneySignId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
