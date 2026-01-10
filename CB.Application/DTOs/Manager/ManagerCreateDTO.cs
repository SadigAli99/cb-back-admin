

using CB.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Manager
{
    public class ManagerCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Fullnames { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Positions { get; set; } = new();
    }
}
