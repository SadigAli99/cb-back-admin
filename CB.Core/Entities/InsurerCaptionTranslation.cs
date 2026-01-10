
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsurerCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InsurerCaptionId { get; set; }
        public InsurerCaption InsurerCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
