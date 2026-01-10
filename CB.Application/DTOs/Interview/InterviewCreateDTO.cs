
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Interview
{
    public class InterviewCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
