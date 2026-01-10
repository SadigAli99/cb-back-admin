
namespace CB.Application.DTOs.NakhchivanBulletin
{
    public class NakhchivanBulletinGetDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
