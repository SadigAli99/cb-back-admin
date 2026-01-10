
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.FutureEvent
{
    public class FutureEventCreateDTO
    {
        public DateTime Date { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Locations { get; set; } = new();
        public Dictionary<string, string> Formats { get; set; } = new();
    }
}
