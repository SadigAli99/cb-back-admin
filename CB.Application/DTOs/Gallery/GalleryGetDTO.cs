
namespace CB.Application.DTOs.Gallery
{
    public class GalleryGetDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Image { get; set; }
        public List<GalleryImageDTO> Images { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> ImageTitles { get; set; } = new();
        public Dictionary<string, string> ImageAlts { get; set; } = new();
    }
}
