
using CB.Core.Enums;


namespace CB.Application.DTOs.Manager
{
    public class ManagerGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public Dictionary<string, string> Fullnames { get; set; } = new();
        public Dictionary<string, string> Positions { get; set; } = new();
    }
}
