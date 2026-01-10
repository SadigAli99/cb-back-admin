
namespace CB.Application.DTOs.TrainingJournalist
{
    public class TrainingJournalistGetDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
        public List<TrainingJournalistImageDTO> Images { get; set; } = new();
    }

}
