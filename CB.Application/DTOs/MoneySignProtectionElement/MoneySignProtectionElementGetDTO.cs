
namespace CB.Application.DTOs.MoneySignProtectionElement
{
    public class MoneySignProtectionElementGetDTO
    {
        public int Id { get; set; }
        public string? MoneySignHistoryTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Values { get; set; } = new();
        public List<MoneySignProtectionElementImageDTO> Images { get; set; } = new();
    }
}
