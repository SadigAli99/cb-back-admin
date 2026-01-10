
namespace CB.Core.Entities
{
    public class PaymentSystemStandartFAQ : BaseEntity
    {
        public int PaymentSystemStandartId { get; set; }
        public PaymentSystemStandart PaymentSystemStandart { get; set; } = null!;
        public List<PaymentSystemStandartFAQTranslation> Translations { get; set; } = new();
    }
}
