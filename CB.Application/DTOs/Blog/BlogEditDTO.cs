
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Blog
{
    public class BlogEditDTO
    {
        public int Id { get; set; }
        public bool IsFocused { get; set; }
        public DateTime Date { get; set; }
        public IFormFile? ImageFile { get; set; }
        public List<IFormFile> Files { get; set; } = new();
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
