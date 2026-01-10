
namespace CB.Application.DTOs.StatisticalReport
{
    public class StatisticalReportGetDTO
    {
        public int Id { get; set; }
        public string? StatisticalReportCategory { get; set; }
        public string? StatisticalReportSubCategory { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Periods { get; set; } = new();
    }
}
