

namespace CB.Application.DTOs.OutOfCoinMoneySignHistory
{
    public class OutOfCoinMoneySignHistoryEditDTO
    {
        public int Id { get; set; }
        public int MoneySignId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
