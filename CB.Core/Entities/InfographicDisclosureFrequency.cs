
namespace CB.Core.Entities
{
    public class InfographicDisclosureFrequency : BaseEntity
    {
        public List<InfographicDisclosureFrequencyTranslation> Translations { get; set; } = new();
    }
}
