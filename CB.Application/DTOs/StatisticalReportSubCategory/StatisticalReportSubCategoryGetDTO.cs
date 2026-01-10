


namespace CB.Application.DTOs.StatisticalReportSubCategory
{
    public class StatisticalReportSubCategoryGetDTO
    {
        public int Id { get; set; }
        public string? StatisticalReportCategory { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
