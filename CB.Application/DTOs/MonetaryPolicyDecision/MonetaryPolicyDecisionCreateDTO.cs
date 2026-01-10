

namespace CB.Application.DTOs.MonetaryPolicyDecision
{
    public class MonetaryPolicyDecisionCreateDTO
    {
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
