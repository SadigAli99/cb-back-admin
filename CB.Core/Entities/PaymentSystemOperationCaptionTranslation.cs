
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentSystemOperationCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int PaymentSystemOperationCaptionId { get; set; }
        public PaymentSystemOperationCaption PaymentSystemOperationCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
