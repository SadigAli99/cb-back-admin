

namespace CB.Application.DTOs.FormerChairman
{
    public class FormerChairmanCreateDTO
    {
        public Dictionary<string, string> Fullnames { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
