
namespace CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristic
{
    public class CurrencyHistoryPrevItemCharacteristicGetDTO
    {
        public int Id { get; set; }
        public string? CurrencyHistoryPrevItemTitle { get; set; }
        public Dictionary<string, string> Labels { get; set; } = new();
        public Dictionary<string, string> Values { get; set; } = new();
    }
}
