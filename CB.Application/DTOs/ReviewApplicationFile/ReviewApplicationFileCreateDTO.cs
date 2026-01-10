
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.ReviewApplicationFile
{
    public class ReviewApplicationFileCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public int ReviewApplicationId { get; set; }
    }
}
