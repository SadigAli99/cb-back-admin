

namespace CB.Application.DTOs.DigitalPaymentInfographic
{
    public class DigitalPaymentInfographicEditDTO
    {
        public int Id { get; set; }
        public int DigitalPaymentInfographicCategoryId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
