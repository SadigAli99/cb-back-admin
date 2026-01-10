
namespace CB.Core.Entities
{
    public class DigitalPaymentInfograhicCategory : BaseEntity
    {
        public List<DigitalPaymentInfograhicCategoryTranslation> Translations { get; set; } = new();
    }
}
