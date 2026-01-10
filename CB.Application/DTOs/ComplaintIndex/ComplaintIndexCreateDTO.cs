
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.ComplaintIndex
{
    public class ComplaintIndexCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
        public int ComplaintIndexCategoryId { get; set; }
    }
}
