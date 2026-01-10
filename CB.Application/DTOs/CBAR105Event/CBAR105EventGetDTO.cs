
namespace CB.Application.DTOs.CBAR105Event
{
    public class CBAR105EventGetDTO
    {
        public List<CBAR105EventImageDTO> Images { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
