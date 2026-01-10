

namespace CB.Core.Entities
{
    public class GovernmentPaymentPortal : BaseEntity
    {
        public List<GovernmentPaymentPortalTranslation> Translations { get; set; } = new();
    }
}
