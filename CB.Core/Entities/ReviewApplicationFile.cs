
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReviewApplicationFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int ReviewApplicationId { get; set; }
        public ReviewApplication ReviewApplication { get; set; } = null!;
        public List<ReviewApplicationFileTranslation> Translations { get; set; } = new();
    }
}
