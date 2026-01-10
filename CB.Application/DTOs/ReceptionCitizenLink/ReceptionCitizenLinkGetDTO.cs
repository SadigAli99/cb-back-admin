
namespace CB.Application.DTOs.ReceptionCitizenLink
{
    public class ReceptionCitizenLinkGetDTO
    {
        public int Id { get; set; }
        public string? ReceptionCitizenCategoryTitle { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
