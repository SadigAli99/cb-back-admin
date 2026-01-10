
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.StatisticalBulletin
{
    public class StatisticalBulletinEditDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
