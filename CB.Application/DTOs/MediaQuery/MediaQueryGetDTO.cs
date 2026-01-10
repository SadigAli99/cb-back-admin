
namespace CB.Application.DTOs.MediaQuery
{
    public class MediaQueryGetDTO
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
