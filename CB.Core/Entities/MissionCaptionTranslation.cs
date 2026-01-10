
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MissionCaptionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(10000)]
        public string? Description { get; set; }
        public int MissionCaptionId { get; set; }
        public MissionCaption MissionCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
