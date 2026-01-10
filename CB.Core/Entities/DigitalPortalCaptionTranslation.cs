
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DigitalPortalCaptionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        public int DigitalPortalCaptionId { get; set; }
        public DigitalPortalCaption DigitalPortalCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
