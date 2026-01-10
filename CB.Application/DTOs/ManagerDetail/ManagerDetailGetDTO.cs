
namespace CB.Application.DTOs.ManagerDetail
{
    public class ManagerDetailGetDTO
    {
        public int Id { get; set; }
        public string? ManagerName { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
