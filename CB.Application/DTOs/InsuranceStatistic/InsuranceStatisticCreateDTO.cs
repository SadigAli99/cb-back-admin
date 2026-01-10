
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.InsuranceStatistic
{
    public class InsuranceStatisticCreateDTO
    {
        public int InsuranceStatisticCategoryId { get; set; }
        public int? InsuranceStatisticSubCategoryId { get; set; }
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
