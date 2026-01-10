
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InternationalCooperationCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InternationalCooperationCaptionId { get; set; }
        public InternationalCooperationCaption InternationalCooperationCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
