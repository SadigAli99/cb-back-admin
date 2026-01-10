
using CB.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Director
{
    public class DirectorEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public DirectorType Type { get; set; }
        public Dictionary<string, string> Fullnames { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Positions { get; set; } = new();
    }
}
