
namespace CB.Application.DTOs.Nomination
{
    public class NominationGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
        public string? NominationCategoryTitle { get; set; }
    }
}
