
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReviewApplicationLink : BaseEntity
    {
        [StringLength(500)]
        public string? Url { get; set; }
        public int ReviewApplicationId { get; set; }
        public ReviewApplication ReviewApplication { get; set; } = null!;
        public List<ReviewApplicationLinkTranslation> Translations { get; set; } = new();
    }
}
