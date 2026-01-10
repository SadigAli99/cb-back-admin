
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CustomerDocumentFile
{
    public class CustomerDocumentFileEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public int CustomerDocumentId { get; set; }
    }
}
