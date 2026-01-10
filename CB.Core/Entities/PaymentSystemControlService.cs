
namespace CB.Core.Entities
{
    public class PaymentSystemControlService : BaseEntity
    {
        public int PaymentSystemControlId { get; set; }
        public PaymentSystemControl PaymentSystemControl { get; set; } = null!;
        public List<PaymentSystemControlServiceTranslation> Translations { get; set; } = new();
    }
}
