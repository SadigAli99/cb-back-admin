
namespace CB.Core.Entities
{
    public class StatisticalReport : BaseEntity
    {
        public int? StatisticalReportCategoryId { get; set; }
        public StatisticalReportCategory? StatisticalReportCategory { get; set; }
        public int? StatisticalReportSubCategoryId { get; set; }
        public StatisticalReportSubCategory? StatisticalReportSubCategory { get; set; }
        public List<StatisticalReportTranslation> Translations { get; set; } = new();
    }
}
