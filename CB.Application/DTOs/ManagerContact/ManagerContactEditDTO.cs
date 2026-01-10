
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.ManagerContact
{
    public class ManagerContactEditDTO
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
