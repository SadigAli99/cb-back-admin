
namespace CB.Application.DTOs.Mission
{
    public class MissionGetDTO
    {
        public int Id { get; set; }
        public string? Icon { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Texts { get; set; } = new();
    }
}
