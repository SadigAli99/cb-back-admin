

namespace CB.Application.DTOs.InstantPaymentFAQ
{
    public class InstantPaymentFAQEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
