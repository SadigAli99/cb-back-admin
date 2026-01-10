
namespace CB.Application.DTOs.CustomerDocumentFile
{
    public class CustomerDocumentFileGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public string? CustomerDocumentTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
