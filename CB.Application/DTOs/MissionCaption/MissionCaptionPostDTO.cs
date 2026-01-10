

namespace CB.Application.DTOs.MissionCaption
{
    public class MissionCaptionPostDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
