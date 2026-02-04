
namespace CB.Core.Entities
{
    public class Gallery : BaseEntity
    {
        public DateTime Date { get; set; }
        public string? Image { get; set; } = null!;
        public List<GalleryTranslation> Translations { get; set; } = new();
        public List<GalleryImage> Images { get; set; } = new();
    }
}
