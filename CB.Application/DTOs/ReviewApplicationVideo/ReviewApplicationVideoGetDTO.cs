
namespace CB.Application.DTOs.ReviewApplicationVideo
{
    public class ReviewApplicationVideoGetDTO
    {
        public int Id { get; set; }
        public string? ReviewApplicationTitle { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
