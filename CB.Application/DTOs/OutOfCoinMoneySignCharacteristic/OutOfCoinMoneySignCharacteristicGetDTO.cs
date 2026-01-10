
namespace CB.Application.DTOs.OutOfCoinMoneySignCharacteristic
{
    public class OutOfCoinMoneySignCharacteristicGetDTO
    {
        public int Id { get; set; }
        public string? MoneySignHistoryTitle { get; set; }
        public Dictionary<string, string> Labels { get; set; } = new();
        public Dictionary<string, string> Values { get; set; } = new();
    }
}
