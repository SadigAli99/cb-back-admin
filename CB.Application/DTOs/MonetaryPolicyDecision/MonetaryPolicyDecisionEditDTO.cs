

namespace CB.Application.DTOs.MonetaryPolicyDecision
{
    public class MonetaryPolicyDecisionEditDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
