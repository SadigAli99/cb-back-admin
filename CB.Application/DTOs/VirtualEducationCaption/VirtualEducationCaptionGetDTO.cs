
namespace CB.Application.DTOs.VirtualEducationCaption
{
    public class VirtualEducationCaptionGetDTO
    {
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
