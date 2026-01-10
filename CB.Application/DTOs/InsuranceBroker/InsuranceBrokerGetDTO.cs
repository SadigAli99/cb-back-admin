
namespace CB.Application.DTOs.InsuranceBroker
{
    public class InsuranceBrokerGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
