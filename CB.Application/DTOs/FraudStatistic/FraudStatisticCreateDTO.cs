
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.FraudStatistic
{
    public class FraudStatisticCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
