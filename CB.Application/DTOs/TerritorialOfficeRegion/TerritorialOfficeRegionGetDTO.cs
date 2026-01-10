
namespace CB.Application.DTOs.TerritorialOfficeRegion
{
    public class TerritorialOfficeRegionGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public string? TerritorialOfficeTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Directors { get; set; } = new();
        public Dictionary<string, string> Locations { get; set; } = new();
    }
}
