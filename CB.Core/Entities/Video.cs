
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Video : BaseEntity
    {
        [StringLength(500)]
        public string? Url { get; set; }
        [StringLength(100)]
        public string? Image { get; set; }
        public DateTime Date { get; set; }
        public List<VideoTranslation> Translations { get; set; } = new();
    }
}
