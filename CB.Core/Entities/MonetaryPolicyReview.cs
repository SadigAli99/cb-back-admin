
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MonetaryPolicyReview : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<MonetaryPolicyReviewTranslation> Translations { get; set; } = new();
    }
}
