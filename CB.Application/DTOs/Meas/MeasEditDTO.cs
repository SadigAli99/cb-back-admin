
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Meas
{
    public class MeasEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public DateTime RegisterTime { get; set; }
        public string? RegisterNumber { get; set; }
        public IFormFile? File { get; set; }
        public int IssuerTypeId { get; set; }
        public int InformationTypeId { get; set; }
        public int SecurityTypeId { get; set; }
    }
}
