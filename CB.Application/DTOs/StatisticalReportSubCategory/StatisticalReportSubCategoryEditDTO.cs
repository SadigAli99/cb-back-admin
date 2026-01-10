

namespace CB.Application.DTOs.StatisticalReportSubCategory
{
    public class StatisticalReportSubCategoryEditDTO
    {
        public int Id { get; set; }
        public int StatisticalReportCategoryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
