
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StatisticalReportCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int StatisticalReportCategoryId { get; set; }
        public StatisticalReportCategory StatisticalReportCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
