

namespace CB.Application.DTOs.MonetaryPolicyGraphic
{
    public class MonetaryPolicyGraphicEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
