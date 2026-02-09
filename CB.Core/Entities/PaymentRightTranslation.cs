

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentRightTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int PaymentRightId { get; set; }
        public PaymentRight? PaymentRight { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
