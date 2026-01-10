
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.ReviewApplicationFile
{
    public class ReviewApplicationFileEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public int ReviewApplicationId { get; set; }
    }
}
