
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StatisticalReportSubCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int StatisticalReportSubCategoryId { get; set; }
        public StatisticalReportSubCategory StatisticalReportSubCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
