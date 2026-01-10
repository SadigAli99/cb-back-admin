
namespace CB.Core.Entities
{
    public class DigitalPaymentInfograhicItem : BaseEntity
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double Value { get; set; }
        public int DigitalPaymentInfograhicId { get; set; }
        public DigitalPaymentInfograhic DigitalPaymentInfograhic { get; set; } = null!;
    }
}
