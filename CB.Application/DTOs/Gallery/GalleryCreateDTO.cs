
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Gallery
{
    public class GalleryCreateDTO
    {
        public DateTime Date { get; set; }
        public IFormFile ImageFile { get; set; } = null!;
        public List<IFormFile> Files { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> ImageTitles { get; set; } = new();
        public Dictionary<string, string> ImageAlts { get; set; } = new();
    }
}
