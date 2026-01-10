
namespace CB.Application.DTOs.DirectorContact
{
    public class DirectorContactGetDTO
    {
        public int Id { get; set; }
        public string? DirectorName { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
