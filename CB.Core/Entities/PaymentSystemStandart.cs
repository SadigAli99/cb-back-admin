
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentSystemStandart : BaseEntity
    {
        public List<PaymentSystemStandartFile> Files { get; set; } = new();
        public List<PaymentSystemStandartTranslation> Translations { get; set; } = new();
    }
}
