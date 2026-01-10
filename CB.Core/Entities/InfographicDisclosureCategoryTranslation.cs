
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InfographicDisclosureCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int InfographicDisclosureCategoryId { get; set; }
        public InfographicDisclosureCategory InfographicDisclosureCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
