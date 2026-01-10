
namespace CB.Application.DTOs.Office
{
    public class OfficeGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Statute { get; set; }
        public string? Phone { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Chairmen { get; set; } = new();
        public Dictionary<string, string> Addresses { get; set; } = new();
    }
}
