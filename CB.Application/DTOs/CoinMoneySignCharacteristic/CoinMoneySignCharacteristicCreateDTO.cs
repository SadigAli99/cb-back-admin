

namespace CB.Application.DTOs.CoinMoneySignCharacteristic
{
    public class CoinMoneySignCharacteristicCreateDTO
    {
        public int MoneySignHistoryId { get; set; }
        public Dictionary<string, string> Labels { get; set; } = new();
        public Dictionary<string, string> Values { get; set; } = new();
    }
}
