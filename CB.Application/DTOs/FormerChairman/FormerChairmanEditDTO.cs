

namespace CB.Application.DTOs.FormerChairman
{
    public class FormerChairmanEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Fullnames { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
