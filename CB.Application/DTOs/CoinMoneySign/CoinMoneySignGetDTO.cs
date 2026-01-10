
namespace CB.Application.DTOs.CoinMoneySign
{
    public class CoinMoneySignGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
