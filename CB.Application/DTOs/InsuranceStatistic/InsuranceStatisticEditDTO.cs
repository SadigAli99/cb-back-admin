
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.InsuranceStatistic
{
    public class InsuranceStatisticEditDTO
    {
        public int Id { get; set; }
        public int InsuranceStatisticCategoryId { get; set; }
        public int? InsuranceStatisticSubCategoryId { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
