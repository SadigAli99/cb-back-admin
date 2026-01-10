


namespace CB.Application.DTOs.DigitalPaymentInfograhic
{
    public class DigitalPaymentInfograhicGetDTO
    {
        public int Id { get; set; }
        public string? DigitalPaymentInfograhicCategoryTitle { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
