


namespace CB.Application.DTOs.StatisticalReportCategory
{
    public class StatisticalReportCategoryGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
