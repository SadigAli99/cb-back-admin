


namespace CB.Application.DTOs.CBAR100Video
{
    public class CBAR100VideoCreateDTO
    {
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
