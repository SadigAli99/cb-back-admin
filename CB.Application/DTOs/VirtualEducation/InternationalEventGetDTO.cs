
namespace CB.Application.DTOs.VirtualEducation
{
    public class VirtualEducationGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
        public List<VirtualEducationImageDTO> Images { get; set; } = new();
    }
}
