


namespace CB.Application.DTOs.DigitalPaymentInfographic
{
    public class DigitalPaymentInfographicGetDTO
    {
        public int Id { get; set; }
        public string? DigitalPaymentInfographicCategoryTitle { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
