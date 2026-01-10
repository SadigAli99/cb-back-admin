
namespace CB.Core.Entities
{
    public class PaymentSystemControl : BaseEntity
    {
        public List<PaymentSystemControlTranslation> Translations { get; set; } = new();
    }
}
