
namespace CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristicImage
{
    public class CurrencyHistoryPrevItemCharacteristicImageGetDTO
    {
        public int Id { get; set; }
        public string? CurrencyHistoryPrevTitle { get; set; }
        public string? FrontImage { get; set; }
        public string? BackImage { get; set; }
        public string? Nominal { get; set; }
        public Dictionary<string, string> Colors { get; set; } = new();
        public Dictionary<string, string> Sizes { get; set; } = new();
    }
}
