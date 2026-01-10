

namespace CB.Application.DTOs.TerritorialOfficeStatistic
{
    public class TerritorialOfficeStatisticCreateDTO
    {
        public int TerritorialOfficeId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
