
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Advertisement
{
    public class AdvertisementCreateDTO
    {
        public DateTime Date { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> MetaTitles { get; set; } = new();
        public Dictionary<string, string> MetaDescriptions { get; set; } = new();
        public Dictionary<string, string> MetaKeywords { get; set; } = new();
        public Dictionary<string, string> ShortDescriptions { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
