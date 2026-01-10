

namespace CB.Application.DTOs.CustomerContact
{
    public class CustomerContactCreateDTO
    {
        public string? Map { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
