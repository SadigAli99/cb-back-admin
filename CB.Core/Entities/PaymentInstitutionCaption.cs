

namespace CB.Core.Entities
{
    public class PaymentInstitutionCaption : BaseEntity
    {
        public List<PaymentInstitutionCaptionTranslation>Translations {get; set; } = new();
    }
}
