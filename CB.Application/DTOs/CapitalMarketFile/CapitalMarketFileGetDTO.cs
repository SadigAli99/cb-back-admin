
namespace CB.Application.DTOs.CapitalMarketFile
{
    public class CapitalMarketFileGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
