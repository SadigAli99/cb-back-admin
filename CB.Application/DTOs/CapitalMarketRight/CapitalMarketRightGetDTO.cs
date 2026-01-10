
namespace CB.Application.DTOs.CapitalMarketRight
{
    public class CapitalMarketRightGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
