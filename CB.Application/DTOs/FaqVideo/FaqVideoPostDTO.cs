

namespace CB.Application.DTOs.FaqVideo
{
    public class FaqVideoPostDTO
    {
        public string? VideoUrl { get; set; }
        public string? PlaylistUrl { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
