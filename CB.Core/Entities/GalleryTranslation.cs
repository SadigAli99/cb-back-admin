

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class GalleryTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(255)]
        public string? ImageTitle { get; set; }
        [StringLength(255)]
        public string? ImageAlt { get; set; }
        public int GalleryId { get; set; }
        public Gallery? Gallery { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
