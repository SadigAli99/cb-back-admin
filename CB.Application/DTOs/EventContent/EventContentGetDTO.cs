
namespace CB.Application.DTOs.EventContent
{
    public class EventContentGetDTO
    {
        public int Id { get; set; }
        public string? EventTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
        public List<EventContentImageDTO> Images { get; set; } = new();
    }
}
