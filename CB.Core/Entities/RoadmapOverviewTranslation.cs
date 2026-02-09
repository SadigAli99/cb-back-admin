

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class RoadmapOverviewTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int RoadmapOverviewId { get; set; }
        public RoadmapOverview? RoadmapOverview { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
