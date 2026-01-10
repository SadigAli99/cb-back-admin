
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.MonetaryPolicyReview
{
    public class MonetaryPolicyReviewCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
