
namespace CB.Application.DTOs.MonetaryPolicyGraphic
{
    public class MonetaryPolicyGraphicGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
