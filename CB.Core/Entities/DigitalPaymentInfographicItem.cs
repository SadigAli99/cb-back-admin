
namespace CB.Core.Entities
{
    public class DigitalPaymentInfographicItem : BaseEntity
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double Value { get; set; }
        public int DigitalPaymentInfographicId { get; set; }
        public DigitalPaymentInfographic DigitalPaymentInfographic { get; set; } = null!;
    }
}
