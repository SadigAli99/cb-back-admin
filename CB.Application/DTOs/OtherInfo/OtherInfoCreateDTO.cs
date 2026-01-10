

namespace CB.Application.DTOs.OtherInfo
{
    public class OtherInfoCreateDTO
    {
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
