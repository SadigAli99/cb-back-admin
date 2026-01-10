

namespace CB.Application.DTOs.OutOfCoinMoneySignCharacteristic
{
    public class OutOfCoinMoneySignCharacteristicCreateDTO
    {
        public int MoneySignHistoryId { get; set; }
        public Dictionary<string, string> Labels { get; set; } = new();
        public Dictionary<string, string> Values { get; set; } = new();
    }
}
