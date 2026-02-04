

namespace CB.Core.Entities
{
    public class InstantPaymentSystem : BaseEntity
    {
        public List<InstantPaymentSystemTranslation>Translations {get; set; } = new();
    }
}
