
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentAgentCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int PaymentAgentCaptionId { get; set; }
        public PaymentAgentCaption PaymentAgentCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
