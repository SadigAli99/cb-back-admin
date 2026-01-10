
namespace CB.Application.DTOs.ClearingSettlementSystemFile
{
    public class ClearingSettlementSystemFileGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public string? ClearingSettlementSystemTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
