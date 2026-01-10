
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class GalleryImage : BaseEntity
    {
        [StringLength(100)]
        public string Image { get; set; } = null!;
        public int GalleryId { get; set; }
        public Gallery Gallery { get; set; } = null!;
    }
}
