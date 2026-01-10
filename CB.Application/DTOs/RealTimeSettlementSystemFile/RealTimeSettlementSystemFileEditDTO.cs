
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.RealTimeSettlementSystemFile
{
    public class RealTimeSettlementSystemFileEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public int RealTimeSettlementSystemId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
