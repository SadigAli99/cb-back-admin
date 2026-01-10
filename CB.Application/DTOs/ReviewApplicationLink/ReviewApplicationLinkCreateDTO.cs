
namespace CB.Application.DTOs.ReviewApplicationLink
{
    public class ReviewApplicationLinkCreateDTO
    {
        public int ReviewApplicationId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
