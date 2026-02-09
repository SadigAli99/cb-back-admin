

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StatisticalReportTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(1000)]
        public string? Period { get; set; }
        public int StatisticalReportId { get; set; }
        public StatisticalReport? StatisticalReport { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
