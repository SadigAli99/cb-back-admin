
namespace CB.Core.Entities
{
    public class StatisticalReportCategory : BaseEntity
    {
        public List<StatisticalReport> Reports { get; set; } = new();
        public List<StatisticalReportSubCategory> SubCategories { get; set; } = new();
        public List<StatisticalReportCategoryTranslation> Translations { get; set; } = new();
    }
}
