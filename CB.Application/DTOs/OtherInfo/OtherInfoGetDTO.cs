
namespace CB.Application.DTOs.OtherInfo
{
    public class OtherInfoGetDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
