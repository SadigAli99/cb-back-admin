
namespace CB.Application.DTOs.LicensingProcess
{
    public class LicensingProcessGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
