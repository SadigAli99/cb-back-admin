
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.PaymentLaw
{
    public class PaymentLawCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
