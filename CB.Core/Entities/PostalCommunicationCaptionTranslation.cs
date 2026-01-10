
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PostalCommunicationCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int PostalCommunicationCaptionId { get; set; }
        public PostalCommunicationCaption PostalCommunicationCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
