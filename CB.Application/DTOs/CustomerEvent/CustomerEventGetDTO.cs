
namespace CB.Application.DTOs.CustomerEvent
{
    public class CustomerEventGetDTO
    {
        public int Id { get; set; }
        public List<CustomerEventImageDTO> Images { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
