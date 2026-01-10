
namespace CB.Application.DTOs.InstantPaymentPost
{
    public class InstantPaymentPostEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> ShortDescriptions { get; set; } = new();
        public Dictionary<string, string> Descrtiptions { get; set; } = new();
    }
}
