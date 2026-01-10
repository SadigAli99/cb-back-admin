

namespace CB.Application.DTOs.DigitalPaymentInfograhic
{
    public class DigitalPaymentInfograhicEditDTO
    {
        public int Id { get; set; }
        public int DigitalPaymentInfograhicCategoryId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
