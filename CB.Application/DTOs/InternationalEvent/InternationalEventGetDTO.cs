
namespace CB.Application.DTOs.InternationalEvent
{
    public class InternationalEventGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
        public List<InternationalEventImageDTO> Images { get; set; } = new();
    }
}
