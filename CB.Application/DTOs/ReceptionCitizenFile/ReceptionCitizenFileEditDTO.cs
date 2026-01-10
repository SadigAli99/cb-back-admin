
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.ReceptionCitizenFile
{
    public class ReceptionCitizenFileEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public int ReceptionCitizenId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
