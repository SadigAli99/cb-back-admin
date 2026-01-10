
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.DirectorContact
{
    public class DirectorContactEditDTO
    {
        public int Id { get; set; }
        public int DirectorId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
