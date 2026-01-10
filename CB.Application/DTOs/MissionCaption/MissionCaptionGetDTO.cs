
namespace CB.Application.DTOs.MissionCaption
{
    public class MissionCaptionGetDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
