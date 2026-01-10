
using CB.Core.Enums;


namespace CB.Application.DTOs.Volunteer
{
    public class VolunteerGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public Dictionary<string, string> Fullnames { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
