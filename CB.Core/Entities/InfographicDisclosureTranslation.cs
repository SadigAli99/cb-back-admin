

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InfographicDisclosureTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Deadline { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InfographicDisclosureId { get; set; }
        public InfographicDisclosure? InfographicDisclosure { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
