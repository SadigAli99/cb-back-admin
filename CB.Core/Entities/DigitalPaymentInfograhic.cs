
namespace CB.Core.Entities
{
    public class DigitalPaymentInfograhic : BaseEntity
    {
        public string? Url { get; set; }
        public int DigitalPaymentInfograhicCategoryId { get; set; }
        public DigitalPaymentInfograhicCategory DigitalPaymentInfograhicCategory { get; set; } = null!;
        public List<DigitalPaymentInfograhicTranslation> Translations { get; set; } = new();
        public List<DigitalPaymentInfograhicItem> DigitalPaymentInfograhicItems { get; set; } = new();
    }
}
