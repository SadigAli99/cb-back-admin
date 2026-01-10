
namespace CB.Core.Entities
{
    public class InfographicDisclosure : BaseEntity
    {
        public int? InfographicDisclosureCategoryId { get; set; }
        public InfographicDisclosureCategory? InfographicDisclosureCategory { get; set; }
        public int? InfographicDisclosureFrequencyId { get; set; }
        public InfographicDisclosureFrequency? InfographicDisclosureFrequency { get; set; }
        public List<InfographicDisclosureTranslation> Translations { get; set; } = new();
    }
}
