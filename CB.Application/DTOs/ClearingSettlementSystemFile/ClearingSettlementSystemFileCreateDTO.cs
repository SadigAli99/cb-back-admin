
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.ClearingSettlementSystemFile
{
    public class ClearingSettlementSystemFileCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public int ClearingSettlementSystemId { get; set; }
    }
}
