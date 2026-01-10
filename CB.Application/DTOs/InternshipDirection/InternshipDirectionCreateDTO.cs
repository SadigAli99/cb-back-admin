

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.InternshipDirection
{
    public class InternshipDirectionCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
