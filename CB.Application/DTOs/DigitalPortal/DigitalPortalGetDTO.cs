
namespace CB.Application.DTOs.DigitalPortal
{
    public class DigitalPortalGetDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Texts { get; set; } = new();
    }
}
