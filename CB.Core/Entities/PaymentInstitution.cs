

namespace CB.Core.Entities
{
    public class PaymentInstitution : BaseEntity
    {
        public List<PaymentInstitutionTranslation> Translations { get; set; } = new();
    }
}
