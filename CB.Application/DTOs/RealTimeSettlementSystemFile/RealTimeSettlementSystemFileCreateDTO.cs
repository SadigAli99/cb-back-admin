
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.RealTimeSettlementSystemFile
{
    public class RealTimeSettlementSystemFileCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public int RealTimeSettlementSystemId { get; set; }
    }
}
