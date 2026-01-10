

namespace CB.Application.DTOs.InfographicDisclosure
{
    public class InfographicDisclosureEditDTO
    {
        public int Id { get; set; }
        public int InfographicDisclosureCategoryId { get; set; }
        public int InfographicDisclosureFrequencyId { get; set; }
        public Dictionary<string, string> Deadlines { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
