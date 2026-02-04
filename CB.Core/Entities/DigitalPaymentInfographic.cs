
namespace CB.Core.Entities
{
    public class DigitalPaymentInfographic : BaseEntity
    {
        public string? Url { get; set; }
        public int DigitalPaymentInfographicCategoryId { get; set; }
        public DigitalPaymentInfographicCategory DigitalPaymentInfographicCategory { get; set; } = null!;
        public List<DigitalPaymentInfographicTranslation> Translations { get; set; } = new();
        public List<DigitalPaymentInfographicItem> DigitalPaymentInfographicItems { get; set; } = new();
    }
}
