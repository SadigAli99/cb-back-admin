
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.TerritorialOfficeRegion
{
    public class TerritorialOfficeRegionCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public string? Phone { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Directors { get; set; } = new();
        public Dictionary<string, string> Locations { get; set; } = new();
        public int TerritorialOfficeId { get; set; }
    }
}
