
namespace CB.Application.DTOs.FraudStatistic
{
    public class FraudStatisticGetDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
