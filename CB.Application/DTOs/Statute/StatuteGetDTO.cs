
namespace CB.Application.DTOs.Statute
{
    public class StatuteGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> SubTitles { get; set; } = new();
    }
}
