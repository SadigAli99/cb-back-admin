
namespace CB.Application.DTOs.StructureCaption
{
    public class StructureCaptionGetDTO
    {
        public string? Image { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
