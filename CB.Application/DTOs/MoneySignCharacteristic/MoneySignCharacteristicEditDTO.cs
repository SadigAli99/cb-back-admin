

namespace CB.Application.DTOs.MoneySignCharacteristic
{
    public class MoneySignCharacteristicEditDTO
    {
        public int Id { get; set; }
        public int MoneySignHistoryId { get; set; }
        public Dictionary<string, string> Labels { get; set; } = new();
        public Dictionary<string, string> Values { get; set; } = new();
    }
}
