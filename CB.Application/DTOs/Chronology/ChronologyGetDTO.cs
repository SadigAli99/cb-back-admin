
namespace CB.Application.DTOs.Chronology
{
    public class ChronologyGetDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
