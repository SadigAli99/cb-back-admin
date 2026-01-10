
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.FraudStatistic
{
    public class FraudStatisticEditDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
