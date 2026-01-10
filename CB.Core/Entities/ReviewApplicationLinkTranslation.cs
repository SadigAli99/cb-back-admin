
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReviewApplicationLinkTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int ReviewApplicationLinkId { get; set; }
        public ReviewApplicationLink ReviewApplicationLink { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
