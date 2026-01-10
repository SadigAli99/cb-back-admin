
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.ExternalSection
{
    public class ExternalSectionEditDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
