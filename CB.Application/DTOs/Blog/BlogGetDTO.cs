
namespace CB.Application.DTOs.Blog
{
    public class BlogGetDTO
    {
        public int Id { get; set; }
        public bool IsFocused { get; set; }
        public DateTime Date { get; set; }
        public string? Image { get; set; }
        public List<BlogImageDTO> Images { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> ImageTitles { get; set; } = new();
        public Dictionary<string, string> ImageAlts { get; set; } = new();
        public Dictionary<string, string> MetaTitles { get; set; } = new();
        public Dictionary<string, string> MetaDescriptions { get; set; } = new();
        public Dictionary<string, string> MetaKeywords { get; set; } = new();
        public Dictionary<string, string> ShortDescriptions { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
