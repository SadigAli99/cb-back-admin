

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MonetaryPolicyReviewTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int MonetaryPolicyReviewId { get; set; }
        public MonetaryPolicyReview? MonetaryPolicyReview { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
