
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Interview
{
    public class InterviewEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
