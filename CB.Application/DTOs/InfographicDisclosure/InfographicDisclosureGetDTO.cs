
namespace CB.Application.DTOs.InfographicDisclosure
{
    public class InfographicDisclosureGetDTO
    {
        public int Id { get; set; }
        public string? InfographicDisclosureCategory { get; set; }
        public string? InfographicDisclosureFrequency { get; set; }
        public Dictionary<string, string> Deadlines { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
