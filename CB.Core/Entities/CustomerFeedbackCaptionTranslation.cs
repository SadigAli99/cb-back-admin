
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerFeedbackCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CustomerFeedbackCaptionId { get; set; }
        public CustomerFeedbackCaption CustomerFeedbackCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
