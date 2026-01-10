
namespace CB.Application.DTOs.RealTimeSettlementSystemFile
{
    public class RealTimeSettlementSystemFileGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public string? RealTimeSettlementSystemTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
