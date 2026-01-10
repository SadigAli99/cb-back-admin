

namespace CB.Application.DTOs.Insurer
{
    public class InsurerCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
