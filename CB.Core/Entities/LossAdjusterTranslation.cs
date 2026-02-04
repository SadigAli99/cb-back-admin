

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LossAdjusterTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int LossAdjusterId { get; set; }
        public LossAdjuster? LossAdjuster { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
