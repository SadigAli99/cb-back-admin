
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.NakhchivanBlog
{
    public class NakhchivanBlogCreateDTO
    {
        public List<IFormFile> Files { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
