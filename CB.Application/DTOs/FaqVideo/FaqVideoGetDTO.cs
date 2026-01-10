
namespace CB.Application.DTOs.FaqVideo
{
    public class FaqVideoGetDTO
    {
        public string? VideoUrl { get; set; }
        public string? PlaylistUrl { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
