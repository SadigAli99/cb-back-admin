
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Office
{
    public class OfficeEditDTO
    {
        public int Id { get; set; }
        public IFormFile? ImageFile { get; set; }
        public IFormFile? StatuteFile { get; set; }
        public string? Phone { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Chairmen { get; set; } = new();
        public Dictionary<string, string> Addresses { get; set; } = new();
    }
}
