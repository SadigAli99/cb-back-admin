

namespace CB.Application.DTOs.InstantPaymentPost
{
    public class InstantPaymentPostCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> ShortDescriptions { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
