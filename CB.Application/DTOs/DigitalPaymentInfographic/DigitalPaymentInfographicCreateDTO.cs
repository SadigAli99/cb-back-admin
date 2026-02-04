

namespace CB.Application.DTOs.DigitalPaymentInfographic
{
    public class DigitalPaymentInfographicCreateDTO
    {
        public int DigitalPaymentInfographicCategoryId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
