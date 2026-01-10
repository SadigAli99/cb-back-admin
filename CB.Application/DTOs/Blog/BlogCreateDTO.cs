
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Blog
{
    public class BlogCreateDTO
    {
        public bool IsFocused { get; set; }
        public DateTime Date { get; set; }
        public IFormFile ImageFile { get; set; } = null!;
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
