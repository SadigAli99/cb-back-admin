
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CustomerDocumentFile
{
    public class CustomerDocumentFileCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public int CustomerDocumentId { get; set; }
    }
}
