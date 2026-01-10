
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.ReceptionCitizenFile
{
    public class ReceptionCitizenFileCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public int ReceptionCitizenId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
