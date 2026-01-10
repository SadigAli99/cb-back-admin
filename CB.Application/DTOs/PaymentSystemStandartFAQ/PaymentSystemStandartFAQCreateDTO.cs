

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.PaymentSystemStandartFAQ
{
    public class PaymentSystemStandartFAQCreateDTO
    {
        public int PaymentSystemStandartId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
