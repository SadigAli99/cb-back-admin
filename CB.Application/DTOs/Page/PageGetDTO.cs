



namespace CB.Application.DTOs.Page
{
    public class PageGetDTO
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Urls { get; set; } = new();
        public Dictionary<string, string> MetaTitles { get; set; } = new();
        public Dictionary<string, string> MetaDescriptions { get; set; } = new();
    }

}
