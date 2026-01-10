
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PublicationCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int PublicationCaptionId { get; set; }
        public PublicationCaption PublicationCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
