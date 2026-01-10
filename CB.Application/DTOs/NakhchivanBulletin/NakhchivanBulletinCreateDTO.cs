
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.NakhchivanBulletin
{
    public class NakhchivanBulletinCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
