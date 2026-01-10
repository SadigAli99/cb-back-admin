

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.PaymentSystemControlService
{
    public class PaymentSystemControlServiceCreateDTO
    {
        public int PaymentSystemControlId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
