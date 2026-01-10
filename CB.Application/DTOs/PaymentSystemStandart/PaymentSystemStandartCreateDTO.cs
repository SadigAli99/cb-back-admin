

namespace CB.Application.DTOs.PaymentSystemStandart
{
    public class PaymentSystemStandartCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
