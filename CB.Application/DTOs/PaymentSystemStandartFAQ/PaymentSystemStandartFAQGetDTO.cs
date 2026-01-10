
namespace CB.Application.DTOs.PaymentSystemStandartFAQ
{
    public class PaymentSystemStandartFAQGetDTO
    {
        public int Id { get; set; }
        public string? PaymentSystemStandartTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
