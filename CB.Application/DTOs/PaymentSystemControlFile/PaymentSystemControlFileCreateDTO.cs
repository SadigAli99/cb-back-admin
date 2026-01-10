
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.PaymentSystemControlFile
{
    public class PaymentSystemControlFileCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
