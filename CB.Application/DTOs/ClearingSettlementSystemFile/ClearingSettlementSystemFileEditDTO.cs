
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.ClearingSettlementSystemFile
{
    public class ClearingSettlementSystemFileEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public int ClearingSettlementSystemId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
