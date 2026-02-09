

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentServiceTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int PaymentServiceId { get; set; }
        public PaymentService? PaymentService { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
