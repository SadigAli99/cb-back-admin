

namespace CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristic
{
    public class CurrencyHistoryPrevItemCharacteristicEditDTO
    {
        public int Id { get; set; }
        public int CurrencyHistoryPrevId { get; set; }
        public Dictionary<string, string> Labels { get; set; } = new();
        public Dictionary<string, string> Values { get; set; } = new();
    }
}
