

namespace CB.Application.DTOs.InstantPaymentFAQ
{
    public class InstantPaymentFAQCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
