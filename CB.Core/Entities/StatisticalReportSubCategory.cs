
namespace CB.Core.Entities
{
    public class StatisticalReportSubCategory : BaseEntity
    {
        public int? StatisticalReportCategoryId { get; set; }
        public StatisticalReportCategory? StatisticalReportCategory { get; set; }
        public List<StatisticalReport> Reports { get; set; } = new();
        public List<StatisticalReportSubCategoryTranslation> Translations { get; set; } = new();
    }
}
