
using CB.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Volunteer
{
    public class VolunteerEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Fullnames { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
