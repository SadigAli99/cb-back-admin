
namespace CB.Application.DTOs.ManagerContact
{
    public class ManagerContactGetDTO
    {
        public int Id { get; set; }
        public string? ManagerName { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
