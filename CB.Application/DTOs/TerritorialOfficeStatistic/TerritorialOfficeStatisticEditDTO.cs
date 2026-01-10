

namespace CB.Application.DTOs.TerritorialOfficeStatistic
{
    public class TerritorialOfficeStatisticEditDTO
    {
        public int Id { get; set; }
        public int TerritorialOfficeId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
