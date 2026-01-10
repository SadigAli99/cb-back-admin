
namespace CB.Application.DTOs.ReviewApplicationLink
{
    public class ReviewApplicationLinkGetDTO
    {
        public int Id { get; set; }
        public string? ReviewApplicationTitle { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
