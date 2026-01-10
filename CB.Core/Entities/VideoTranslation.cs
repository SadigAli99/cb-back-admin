
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class VideoTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int VideoId { get; set; }
        public Video Video { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
