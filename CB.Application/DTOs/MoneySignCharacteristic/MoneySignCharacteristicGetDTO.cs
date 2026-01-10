
namespace CB.Application.DTOs.MoneySignCharacteristic
{
    public class MoneySignCharacteristicGetDTO
    {
        public int Id { get; set; }
        public string? MoneySignHistoryTitle { get; set; }
        public Dictionary<string, string> Labels { get; set; } = new();
        public Dictionary<string, string> Values { get; set; } = new();
    }
}
