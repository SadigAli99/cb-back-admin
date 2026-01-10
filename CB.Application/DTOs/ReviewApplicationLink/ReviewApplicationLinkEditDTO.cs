
namespace CB.Application.DTOs.ReviewApplicationLink
{
    public class ReviewApplicationLinkEditDTO
    {
        public int Id { get; set; }
        public int ReviewApplicationId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
