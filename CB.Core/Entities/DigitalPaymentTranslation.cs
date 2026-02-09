

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DigitalPaymentTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int DigitalPaymentId { get; set; }
        public DigitalPayment? DigitalPayment { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
