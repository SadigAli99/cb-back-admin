
namespace CB.Application.DTOs.NakhchivanEvent
{
    public class NakhchivanEventGetDTO
    {
        public int Id { get; set; }
        public List<NakhchivanEventImageDTO> Images { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
