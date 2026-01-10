

namespace CB.Application.DTOs.StatisticalReportSubCategory
{
    public class StatisticalReportSubCategoryCreateDTO
    {
        public int StatisticalReportCategoryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
