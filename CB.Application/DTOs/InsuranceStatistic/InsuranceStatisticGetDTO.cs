
namespace CB.Application.DTOs.InsuranceStatistic
{
    public class InsuranceStatisticGetDTO
    {
        public int Id { get; set; }
        public string? InsuranceStatisticCategory { get; set; }
        public string? InsuranceStatisticSubCategory { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();

    }
}
