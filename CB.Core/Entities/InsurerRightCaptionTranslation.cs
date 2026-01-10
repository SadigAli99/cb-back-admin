
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsurerRightCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InsurerRightCaptionId { get; set; }
        public InsurerRightCaption InsurerRightCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
