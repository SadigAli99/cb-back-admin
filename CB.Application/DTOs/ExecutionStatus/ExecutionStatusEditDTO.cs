
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.ExecutionStatus
{
    public class ExecutionStatusEditDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
