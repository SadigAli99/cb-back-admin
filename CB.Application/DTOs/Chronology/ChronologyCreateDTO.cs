

namespace CB.Application.DTOs.Chronology
{
    public class ChronologyCreateDTO
    {
        public int Year { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
