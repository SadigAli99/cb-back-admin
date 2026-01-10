
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ShareholderCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int ShareholderCaptionId { get; set; }
        public ShareholderCaption ShareholderCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
