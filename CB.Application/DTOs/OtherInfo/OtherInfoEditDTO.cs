
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.OtherInfo
{
    public class OtherInfoEditDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
