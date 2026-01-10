
namespace CB.Core.Entities
{
    public class InstantPaymentFAQ : BaseEntity
    {
        public List<InstantPaymentFAQTranslation> Translations { get; set; } = new();
    }
}
