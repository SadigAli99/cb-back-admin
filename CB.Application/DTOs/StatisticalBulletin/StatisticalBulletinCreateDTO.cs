
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.StatisticalBulletin
{
    public class StatisticalBulletinCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
