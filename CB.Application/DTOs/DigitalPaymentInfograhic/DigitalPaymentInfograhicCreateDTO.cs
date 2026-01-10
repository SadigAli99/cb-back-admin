

namespace CB.Application.DTOs.DigitalPaymentInfograhic
{
    public class DigitalPaymentInfograhicCreateDTO
    {
        public int DigitalPaymentInfograhicCategoryId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
