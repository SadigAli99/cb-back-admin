
namespace CB.Application.DTOs.CBAR100Video
{
    public class CBAR100VideoGetDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
