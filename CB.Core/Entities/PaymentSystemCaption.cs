

namespace CB.Core.Entities
{
    public class PaymentSystemCaption : BaseEntity
    {
        public List<PaymentSystemCaptionTranslation>Translations {get; set; } = new();
    }
}
