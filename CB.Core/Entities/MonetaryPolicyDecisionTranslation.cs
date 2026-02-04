

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MonetaryPolicyDecisionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Slug { get; set; }
        [StringLength(20000)]
        public string? Description { get; set; }
        public int MonetaryPolicyDecisionId { get; set; }
        public MonetaryPolicyDecision? MonetaryPolicyDecision { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
