

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.PageDetail
{
    public class PageDetailEditDTO
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public int PageId { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Urls { get; set; } = new();
        public Dictionary<string, string> MetaTitles { get; set; } = new();
        public Dictionary<string, string> MetaDescriptions { get; set; } = new();
    }
}
