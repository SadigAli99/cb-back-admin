
namespace CB.Core.Entities
{
    public class DigitalPaymentInfographicCategory : BaseEntity
    {
        public List<DigitalPaymentInfographicCategoryTranslation> Translations { get; set; } = new();
    }
}
