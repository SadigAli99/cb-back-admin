
namespace CB.Application.DTOs.ReviewApplicationVideo
{
    public class ReviewApplicationVideoEditDTO
    {
        public int Id { get; set; }
        public int ReviewApplicationId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
