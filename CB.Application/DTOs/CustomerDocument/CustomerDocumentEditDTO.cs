
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CustomerDocument
{
    public class CustomerDocumentEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
