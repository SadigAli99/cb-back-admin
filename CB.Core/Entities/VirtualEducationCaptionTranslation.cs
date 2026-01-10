
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class VirtualEducationCaptionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int VirtualEducationCaptionId { get; set; }
        public VirtualEducationCaption VirtualEducationCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
