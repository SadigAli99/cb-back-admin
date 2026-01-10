
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.AttestationFile
{
    public class AttestationFileCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public int AttestationId { get; set; }
    }
}
