
namespace CB.Application.DTOs.MacroDocument
{
    public class MacroDocumentGetDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Texts { get; set; } = new();
    }
}
