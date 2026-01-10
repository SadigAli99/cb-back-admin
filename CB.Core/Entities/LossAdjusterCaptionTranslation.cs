
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LossAdjusterCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int LossAdjusterCaptionId { get; set; }
        public LossAdjusterCaption LossAdjusterCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
