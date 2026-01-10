
namespace CB.Application.DTOs.TerritorialOfficeStatistic
{
    public class TerritorialOfficeStatisticGetDTO
    {
        public int Id { get; set; }
        public string? TerritorialOfficeTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
