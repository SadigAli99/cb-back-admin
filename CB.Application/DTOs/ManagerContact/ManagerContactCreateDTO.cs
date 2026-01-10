

namespace CB.Application.DTOs.ManagerContact
{
    public class ManagerContactCreateDTO
    {
        public int ManagerId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
