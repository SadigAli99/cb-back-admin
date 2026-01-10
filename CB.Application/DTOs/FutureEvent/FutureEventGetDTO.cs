
namespace CB.Application.DTOs.FutureEvent
{
    public class FutureEventGetDTO
    {
        public int Id { get; set; }
        public string? Date { get; set; } = string.Empty;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Locations { get; set; } = new();
        public Dictionary<string, string> Formats { get; set; } = new();
    }
}
