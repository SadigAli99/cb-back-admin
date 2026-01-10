
namespace CB.Application.DTOs.AttestationFile
{
    public class AttestationFileGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public string? AttestationTitle { get; set; }
    }
}
