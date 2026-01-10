
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OtherRightCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int OtherRightCaptionId { get; set; }
        public OtherRightCaption OtherRightCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
