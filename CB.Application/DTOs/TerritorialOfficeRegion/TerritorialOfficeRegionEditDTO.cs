
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.TerritorialOfficeRegion
{
    public class TerritorialOfficeRegionEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public string? Phone { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Directors { get; set; } = new();
        public Dictionary<string, string> Locations { get; set; } = new();
        public int TerritorialOfficeId { get; set; }
    }
}
