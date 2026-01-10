
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InfographicDisclosureFrequencyTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int InfographicDisclosureFrequencyId { get; set; }
        public InfographicDisclosureFrequency InfographicDisclosureFrequency { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
