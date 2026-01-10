
namespace CB.Application.DTOs.Video
{
    public class VideoGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Url { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
