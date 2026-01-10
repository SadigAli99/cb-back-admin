
namespace CB.Application.DTOs.ReceptionCitizenLink
{
    public class ReceptionCitizenLinkCreateDTO
    {
        public int ReceptionCitizenCategoryId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
