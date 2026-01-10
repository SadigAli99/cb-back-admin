
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NominationCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int NominationCaptionId { get; set; }
        public NominationCaption NominationCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
