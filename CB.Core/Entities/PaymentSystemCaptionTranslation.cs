
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentSystemCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int PaymentSystemCaptionId { get; set; }
        public PaymentSystemCaption PaymentSystemCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
