

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InstantPaymentPostTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Slug { get; set; }
        [StringLength(1000)]
        public string? ShortDescription { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InstantPaymentPostId { get; set; }
        public InstantPaymentPost? InstantPaymentPost { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
