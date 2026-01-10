
namespace CB.Application.DTOs.CBAR100Video
{
    public class CBAR100VideoEditDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
