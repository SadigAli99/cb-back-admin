

namespace CB.Application.DTOs.StatisticalReportCategory
{
    public class StatisticalReportCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
