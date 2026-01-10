
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.InternshipDirection
{
    public class InternshipDirectionEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
