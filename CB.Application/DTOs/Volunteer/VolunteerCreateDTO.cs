

using CB.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Volunteer
{
    public class VolunteerCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Fullnames { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
