

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MonetaryPolicyGraphicTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int MonetaryPolicyGraphicId { get; set; }
        public MonetaryPolicyGraphic? MonetaryPolicyGraphic { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
