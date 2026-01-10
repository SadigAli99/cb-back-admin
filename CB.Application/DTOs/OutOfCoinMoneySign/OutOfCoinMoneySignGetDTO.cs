
namespace CB.Application.DTOs.OutOfCoinMoneySign
{
    public class OutOfCoinMoneySignGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
