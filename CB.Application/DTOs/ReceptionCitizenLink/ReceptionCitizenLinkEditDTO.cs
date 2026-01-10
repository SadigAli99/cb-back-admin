
namespace CB.Application.DTOs.ReceptionCitizenLink
{
    public class ReceptionCitizenLinkEditDTO
    {
        public int Id { get; set; }
        public int ReceptionCitizenCategoryId { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
