

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentLawTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int PaymentLawId { get; set; }
        public PaymentLaw? PaymentLaw { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
