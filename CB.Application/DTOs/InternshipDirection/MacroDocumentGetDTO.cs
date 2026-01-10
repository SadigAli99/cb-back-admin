
namespace CB.Application.DTOs.InternshipDirection
{
    public class InternshipDirectionGetDTO
    {
        public int Id { get; set; }
        public string? Icon { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
