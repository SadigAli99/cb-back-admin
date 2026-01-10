
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class RoadmapSustainableFinanceTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int RoadmapSustainableFinanceId { get; set; }
        public RoadmapSustainableFinance RoadmapSustainableFinance { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
