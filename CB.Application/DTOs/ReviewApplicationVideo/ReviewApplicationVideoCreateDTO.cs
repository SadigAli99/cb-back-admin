
namespace CB.Application.DTOs.ReviewApplicationVideo
{
    public class ReviewApplicationVideoCreateDTO
    {
        public int ReviewApplicationId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
