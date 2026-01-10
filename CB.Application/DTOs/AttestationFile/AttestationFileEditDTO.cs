
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.AttestationFile
{
    public class AttestationFileEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public int AttestationId { get; set; }
    }
}
