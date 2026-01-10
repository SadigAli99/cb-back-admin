
namespace CB.Application.DTOs.ReceptionCitizenFile
{
    public class ReceptionCitizenFileGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public string? ReceptionCitizenTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
