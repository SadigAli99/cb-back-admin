
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Meas
{
    public class MeasCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public DateTime RegisterTime { get; set; }
        public string? RegisterNumber { get; set; }
        public IFormFile File { get; set; } = null!;
        public int IssuerTypeId { get; set; }
        public int InformationTypeId { get; set; }
        public int SecurityTypeId { get; set; }
    }
}
