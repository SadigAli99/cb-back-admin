
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MediaCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int MediaCaptionId { get; set; }
        public MediaCaption MediaCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
