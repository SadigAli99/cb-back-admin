

namespace CB.Application.DTOs.StatisticalReport
{
    public class StatisticalReportCreateDTO
    {
        public int StatisticalReportCategoryId { get; set; }
        public int? StatisticalReportSubCategoryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Periods { get; set; } = new();
    }
}
