
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReviewApplicationVideo : BaseEntity
    {
        [StringLength(500)]
        public string? Url { get; set; }
        public int ReviewApplicationId { get; set; }
        public ReviewApplication ReviewApplication { get; set; } = null!;
        public List<ReviewApplicationVideoTranslation> Translations { get; set; } = new();
    }
}
