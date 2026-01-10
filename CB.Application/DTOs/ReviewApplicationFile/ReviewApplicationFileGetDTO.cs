
namespace CB.Application.DTOs.ReviewApplicationFile
{
    public class ReviewApplicationFileGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public string? ReviewApplicationTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
