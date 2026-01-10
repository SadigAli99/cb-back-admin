
namespace CB.Application.DTOs.DigitalPaymentInfograhicItem
{
    public class DigitalPaymentInfograhicItemGetDTO
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double Value { get; set; }
        public string? DigitalPaymentInfograhicTitle { get; set; }
    }
}
