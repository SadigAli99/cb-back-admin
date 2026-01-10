

namespace CB.Core.Entities
{
    public class PaymentSystemOperation : BaseEntity
    {
        public List<PaymentSystemOperationTranslation> Translations { get; set; } = new();
    }
}
